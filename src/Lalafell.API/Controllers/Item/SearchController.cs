using Lalafell.API.Data.Dto;
using Lalafell.API.Infrastructure.Lumina;
using Lalafell.API.Infrastructure.Lumina.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Lalafell.API.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ItemProvider _itemProvider;

        public SearchController(ItemProvider itemProvider)
        {
            _itemProvider = itemProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemSearchResultDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult Get([FromQuery(Name = "string")] string query, [FromQuery] int limit)
        {
            try
            {
                var searchQuery = query.ToLower();
                var resultsQuery = _itemProvider.GetSearchItems()
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