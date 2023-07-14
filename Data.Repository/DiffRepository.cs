using Application.Service.Interface.Repositories;
using Data.Repository.Context;
using Domain.Model.DTO;
using Domain.Model.Entities;
using Domain.Model.Enuns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DiffRepository : IDiffRepository
    {
        private readonly DiffContext _context;

        public DiffRepository(DiffContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ItemDiff>> GetAllAsync()
        {
            return await _context.ItemDiff.ToListAsync();
        }

        public async Task<ItemDiff> GetByIdAsync(long id)
        {
            return await _context.ItemDiff.FindAsync(id);
        }

        public async Task AddOrUpdateAsync(ItemDiff itemDiff)
        {
            if (itemDiff == null)
                throw new ArgumentNullException(nameof(itemDiff));

            ItemDiff dbDiffItem = await _context.ItemDiff.FindAsync(itemDiff.Id);

            if (dbDiffItem == null)
            {
                _context.ItemDiff.Add(itemDiff);
            }
            else
            {
                dbDiffItem.Left = itemDiff.Left;
                dbDiffItem.Right = itemDiff.Right;
            }

            await _context.SaveChangesAsync();
        }

        public async Task PutByIdAsync(long id, Side side, string decodedData)
        {
            ItemDiff dbDiffItem = await _context.ItemDiff.FindAsync(id);

            if (dbDiffItem == null)
            {
                dbDiffItem = new ItemDiff
                {
                    Id = id
                };

                _context.ItemDiff.Add(dbDiffItem);
            }

            switch (side)
            {
                case Side.Left:
                    dbDiffItem.Left = decodedData;
                    break;

                case Side.Right:
                    dbDiffItem.Right = decodedData;
                    break;

                default:
                    throw new ArgumentException("Invalid side parameter.");
            }

            await _context.SaveChangesAsync();
        }

    }

}
