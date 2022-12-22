using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service.Interface
{
    public interface IDiffService
    {
        Task<IEnumerable<ItemDiff>> GetAll();
        Task<ResultDiff> GetById(long id);
        void PutById(long id, string side, InputDiff diffInput);
    }
}
