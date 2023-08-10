using Newtonsoft.Json;

namespace Lalafell.API.Data.Dto;

public sealed record ClassJobCategoryDto
{
    [JsonProperty("ID")]
    public int Id { get; init; }

    [JsonProperty("Name")]
    public string Name { get; init; } = string.Empty;
}