using Newtonsoft.Json;

namespace Lalafell.API.Data.Dto;

public sealed record PaginationDto
{
    [JsonProperty("Results")]
    public int Results { get; init; }

    [JsonProperty("ResultsTotal")]
    public int ResultsTotal { get; init; } 
}