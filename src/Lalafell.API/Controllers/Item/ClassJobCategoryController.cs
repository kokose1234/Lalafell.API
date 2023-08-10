using Lalafell.API.Data.Dto;
using Lalafell.API.Infrastructure.Lumina.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Lalafell.API.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassJobCategoryController : ControllerBase
    {
        private readonly ItemProvider _itemProvider;

        public ClassJobCategoryController(ItemProvider itemProvider)
        {
            _itemProvider = itemProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemSearchCategoryDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_itemProvider.GetClassJobCategories());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}