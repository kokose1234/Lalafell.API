using Newtonsoft.Json;

namespace XIVAPI.KR.Data.Dto;

public sealed record PaginationDto
{
    [JsonProperty("Results")]
    public int Results { get; init; }

    [JsonProperty("ResultsTotal")]
    public int ResultsTotal { get; init; } 
}