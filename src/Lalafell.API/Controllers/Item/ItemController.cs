using Lalafell.API.Data.Dto;
using Lalafell.API.Infrastructure.Lumina;
using Lalafell.API.Infrastructure.Lumina.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Lalafell.API.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemProvider _itemProvider;

        public ItemController(ItemProvider itemProvider)
        {
            _itemProvider = itemProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult GetAllItems()
        {
            try
            {
                return Ok(_itemProvider.GetItems());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}