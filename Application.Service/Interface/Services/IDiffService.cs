using Domain.Model.Entities;
using Domain.Model.Enuns;

namespace Application.Service.Interface
{
    public interface IDiffService
    {
        Task<IEnumerable<ItemDiff>> GetAll();
        Task<ResultDiff> GetById(long id);
        void PutById(long id, Side side, InputDiff diffInput);
    }
}
