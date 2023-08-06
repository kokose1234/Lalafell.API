using Microsoft.AspNetCore.Mvc;
using XIVAPI.KR.Data.Dto;
using XIVAPI.KR.Services;

namespace XIVAPI.KR.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly LuminaProvider _lumina;

        public SearchController(LuminaProvider lumina)
        {
            _lumina = lumina;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemSearchResultDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult Get([FromQuery(Name = "string")] string query, [FromQuery] int limit)
        {
            try
            {
                var searchQuery = query.ToLower();
                var resultsQuery = _lumina.GetSearchItems()
                                          .Where(i => i.Value.Name.ToLower().Contains(searchQuery))
                                          .OrderByDescending(x => x.Value.ItemLevel)
                                          .Select(i => i.Value);
                var results = resultsQuery.ToList();
                var count = results.Count;
                var pagination = new PaginationDto
                {
                    Results = count > limit ? limit : count,
                    ResultsTotal = count
                };
                var dto = new ItemSearchResultDto {Pagination = pagination, Results = results.Take(limit)};

                return Ok(dto);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}