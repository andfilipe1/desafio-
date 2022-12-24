using Domain.Model.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.Model.Entities
{
    public class ResultDiff
    {
   
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeDiffResult TypeDiffResult { get; set; }

    
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TypeDiff> Diffs { get; set; }
    }
}
