using Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Context
{
    public class DiffContext : DbContext
    {
        public DiffContext(DbContextOptions<DiffContext> options)
            : base(options)
        {
        }
        public DbSet<ItemDiff> ItemDiff { get; set; }
    }

}
