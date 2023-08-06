using Microsoft.AspNetCore.Mvc;
using XIVAPI.KR.Data.Dto;
using XIVAPI.KR.Services;

namespace XIVAPI.KR.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemUICategoryController : ControllerBase
    {
        private readonly LuminaProvider _lumina;

        public ItemUICategoryController(LuminaProvider lumina)
        {
            _lumina = lumina;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemUICategoryDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_lumina.GetItemUICategories());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}