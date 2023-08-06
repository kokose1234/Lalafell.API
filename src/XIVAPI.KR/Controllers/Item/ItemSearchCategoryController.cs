using Microsoft.AspNetCore.Mvc;
using XIVAPI.KR.Data.Dto;
using XIVAPI.KR.Services;

namespace XIVAPI.KR.Controllers.Item
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSearchCategoryController : ControllerBase
    {
        private readonly LuminaProvider _lumina;

        public ItemSearchCategoryController(LuminaProvider lumina)
        {
            _lumina = lumina;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemSearchCategoryDto>), 200, "application/json")]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_lumina.GetItemSearchCategories());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}