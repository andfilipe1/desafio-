using Application.Service.Interface.Repositories;
using Data.Repository.Context;
using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class DiffRepository : IDiffRepository
    {
        private readonly DiffContext _context;

        public DiffRepository(DiffContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemDiff>> GetAll()
        {
            return await _context.ItemDiff.ToListAsync();
        }

        public async Task<ItemDiff> GetById(long id)
        {
            return await _context.ItemDiff.FindAsync(id);
        }

        public void PutById(long id, string side, string decodedData)
        {
            ItemDiff dbDiffItem = _context.ItemDiff.Find(id);

            //Create new item in base
            if (dbDiffItem == null)
            {
                _context.ItemDiff.Add(new ItemDiff
                {
                    Id = id,
                    Left = (side == "left") ? decodedData : null,
                    Right = (side == "right") ? decodedData : null
                });
                _context.SaveChangesAsync();
            }
            else
            {
                //Update item in base
                if (side == "left") dbDiffItem.Left = decodedData;
                else if (side == "right") dbDiffItem.Right = decodedData;
                _context.SaveChangesAsync();
            }
        }
    }
}