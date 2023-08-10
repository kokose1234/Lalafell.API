using Lalafell.API.Data.Dto;
using Lalafell.API.Infrastructure.Lumina;
using Lalafell.API.Infrastructure.Lumina.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Lalafell.API.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSearchCategoryController : ControllerBase
    {
        private readonly ItemProvider _itemProvider;

        public ItemSearchCategoryController(ItemProvider itemProvider)
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
                return Ok(_itemProvider.GetItemSearchCategories());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}