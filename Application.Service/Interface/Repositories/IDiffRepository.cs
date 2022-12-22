using Domain.Model.Entities;

namespace Application.Service.Interface.Repositories
{
    public interface IDiffRepository
    {
        Task<IEnumerable<ItemDiff>> GetAll();
        Task<ItemDiff> GetById(long id);
        void PutById(long id, string side, string decodedData);
    }
}
