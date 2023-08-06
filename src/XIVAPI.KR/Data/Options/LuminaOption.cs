using Newtonsoft.Json;

namespace XIVAPI.KR.Data.Options;

public sealed record LuminaOption
{
    public string DataPath { get; init; } = string.Empty;
}