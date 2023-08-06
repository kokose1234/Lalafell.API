using Newtonsoft.Json;

namespace XIVAPI.KR.Data.Dto;

public sealed record ItemSearchDto
{
    [JsonProperty("ID")]
    public int Id { get; init; }

    [JsonProperty("Icon")]
    public string Icon { get; init; } = string.Empty;

    [JsonProperty("ItemKind")]
    public ItemKind Kind { get; init; } = new();

    [JsonProperty("ItemSearchCategory")]
    public ItemSearchCategory Category { get; init; } = new();

    [JsonProperty("LevelItem")]
    public int ItemLevel { get; init; }

    [JsonProperty("Name")]
    public string Name { get; init; } = string.Empty;

    [JsonProperty("Rarity")]
    public int Rarity { get; init; }


    public sealed record ItemKind
    {
        [JsonProperty("Name")]
        public string Name { get; init; } = string.Empty;
    }

    public record ItemSearchCategory
    {
        [JsonProperty("ID")]
        public int Id { get; init; }

        [JsonProperty("Name")]
        public string Name { get; init; } = string.Empty;
    }
}