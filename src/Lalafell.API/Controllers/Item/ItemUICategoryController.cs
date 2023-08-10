using Lalafell.API.Data.Dto;
using Lalafell.API.Infrastructure.Lumina;
using Lalafell.API.Infrastructure.Lumina.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Lalafell.API.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemUICategoryController : ControllerBase
    {
        private readonly ItemProvider _itemProvider;

        public ItemUICategoryController(ItemProvider itemProvider)
        {
            _itemProvider = itemProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemUICategoryDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_itemProvider.GetItemUICategories());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}