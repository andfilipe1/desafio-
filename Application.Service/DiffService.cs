using Application.Service.Interface;
using Application.Service.Interface.Repositories;
using Domain.Model.DTO;
using Domain.Model.Entities;
using Domain.Model.Enuns;
using System.Text;

namespace Application.Service
{
    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffRepository;

        public DiffService(IDiffRepository diffRepository)
        {
            _diffRepository = diffRepository ?? throw new ArgumentNullException(nameof(diffRepository));
        }

        // Retrieves all ItemDiff entities
        public async Task<IEnumerable<ItemDiff>> GetAll()
        {
            return await _diffRepository.GetAllAsync();
        }

        // Retrieves a specific ItemDiff entity by ID and returns the ResultDiff
        public async Task<ResultDiff> GetById(long id)
        {
            ItemDiff itemDiff = await _diffRepository.GetByIdAsync(id);

            if (itemDiff == null)
                return new ResultDiff { TypeDiffResult = TypeDiffResult.Empty };

            return VerificaItemDiff(itemDiff);
        }

        // Updates the left or right side of an ItemDiff entity based on the provided side and input data
        public void PutById(long id, Side side, InputDiff inputDiff)
        {
            // Decode the input data
            var dataDecode = Encoding.ASCII.GetString(Convert.FromBase64String(inputDiff.Data));

            // Database call to update the entity
            _diffRepository.PutByIdAsync(id, side, dataDecode).Wait();
        }

        // Compares the left and right sides of an ItemDiff entity and returns the ResultDiff
        private static ResultDiff VerificaItemDiff(ItemDiff itemDiff)
        {
            // Compare if any side is empty
            if (string.IsNullOrEmpty(itemDiff.Left) || string.IsNullOrEmpty(itemDiff.Right))
                return new ResultDiff { TypeDiffResult = TypeDiffResult.NotPossibleCompare };

            // Compare if the sides are equal
            if (itemDiff.Left == itemDiff.Right)
                return new ResultDiff { TypeDiffResult = TypeDiffResult.Equals };

            // Compare if the field size is different between the sides
            if (itemDiff.Left.Length != itemDiff.Right.Length)
                return new ResultDiff { TypeDiffResult = TypeDiffResult.SizeDoNotMatch };

            return Different(itemDiff);
        }

        // Differentiates between the left and right sides of an ItemDiff entity and returns the ResultDiff
        private static ResultDiff Different(ItemDiff itemDiff)
        {
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
                {
                    lastEqualIndex = i;
                }
                else if (i - lastEqualIndex > 1)
                {
                    resultDiff.Diffs[resultDiff.Diffs.Count - 1].Length++;
                }
                else
                {
                    resultDiff.Diffs.Add(new TypeDiff { Offset = i, Length = 1 });
                }
            }

            return resultDiff;
        }
    }
}
