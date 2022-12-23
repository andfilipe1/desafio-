using Application.Service.Interface;
using Application.Service.Interface.Repositories;
using Domain.Model.DTO;
using Domain.Model.Entities;
using System.Text;

namespace Application.Service
{

    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffRepository;

        public DiffService(IDiffRepository diffRepository)
        {
            _diffRepository = diffRepository;
        }
        public async Task<IEnumerable<ItemDiff>> GetAll()
        {
            return await _diffRepository.GetAll();
        }

        public async Task<ResultDiff> GetById(long id)
        {
            ItemDiff itemDiff = await _diffRepository.GetById(id);
            
            switch (itemDiff)
            {
                case null:
                    return new ResultDiff { TypeDiffResult = TypeDiffResult.Empty };

                case ItemDiff:
                    return VerificaItemDiff(itemDiff);
            }
        }

        public void PutById(long id, string side, InputDiff inputDiff)
        {
            //Decode the input data
            var dataDecode = Encoding.ASCII.GetString(Convert.FromBase64String(inputDiff.Data));
            //Database call
            _diffRepository.PutById(id, side, dataDecode);
        }

        private static ResultDiff VerificaItemDiff(ItemDiff itemDiff)
        {
                if (itemDiff.Left is null || itemDiff.Right is null)
                    return new ResultDiff { TypeDiffResult = TypeDiffResult.NotPossibleCompare };

                if (itemDiff.Left == itemDiff.Right)
                    return new ResultDiff { TypeDiffResult = TypeDiffResult.Equals };

                if (itemDiff.Left.Length != itemDiff.Right.Length)
                    return new ResultDiff { TypeDiffResult = TypeDiffResult.SizeDoNotMatch };

            return different(itemDiff);

        }

        private static ResultDiff different(ItemDiff itemDiff)
        {
            // Differentiate between sides
            var resultDiff = new ResultDiff
            {
                Diffs = new List<TypeDiff>(),
                TypeDiffResult = TypeDiffResult.ContentDoNotMatch
            };
            var left = itemDiff.Left.ToCharArray();
            var right = itemDiff.Right.ToCharArray();
            int lastEqualIndex = -1;
                for (int i = 0; i < left.Length; i++)
                {
                    if (left[i] == right[i])
                        lastEqualIndex = i;
                    else if (i - lastEqualIndex > 1)
                        resultDiff.Diffs[resultDiff.Diffs.Count - 1].Length++;
                    else
                        resultDiff.Diffs.Add(new TypeDiff { Offset = i, Length = 1 });
                }
            return resultDiff;
        }


    }
}

