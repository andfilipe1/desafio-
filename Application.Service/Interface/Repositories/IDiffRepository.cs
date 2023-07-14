using Domain.Model.DTO;
using Domain.Model.Entities;
using Domain.Model.Enuns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service.Interface.Repositories
{
    public interface IDiffRepository
    {
        Task<IEnumerable<ItemDiff>> GetAllAsync();
        Task<ItemDiff> GetByIdAsync(long id);
        Task AddOrUpdateAsync(ItemDiff itemDiff);
        Task PutByIdAsync(long id, Side side, string decodedData);
    }
}
