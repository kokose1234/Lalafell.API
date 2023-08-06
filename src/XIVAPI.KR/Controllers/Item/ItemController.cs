using Microsoft.AspNetCore.Mvc;
using XIVAPI.KR.Data.Dto;
using XIVAPI.KR.Services;

namespace XIVAPI.KR.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly LuminaProvider _lumina;

        public ItemController(LuminaProvider lumina)
        {
            _lumina = lumina;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult GetAllItems()
        {
            try
            {
                return Ok(_lumina.GetItems());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}