﻿using Newtonsoft.Json;

namespace Lalafell.API.Data.Dto;

public sealed record ItemSearchResultDto
{
    [JsonProperty("Pagination")]
    public PaginationDto Pagination { get; init; } = new();

    [JsonProperty("Results")]
    public IEnumerable<ItemSearchDto> Results { get; init; } = Enumerable.Empty<ItemSearchDto>();
}