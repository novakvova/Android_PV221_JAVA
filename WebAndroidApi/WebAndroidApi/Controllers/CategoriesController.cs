using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAndroidApi.Models.Category;

namespace WebAndroidApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static List<CategoryItemViewModel> _categories;
        public CategoriesController()
        {
            _categories = new List<CategoryItemViewModel>()
            {
                new CategoryItemViewModel
                {
                    Id = 1,
                    Name = "Тривалі в роботі",
                    Image = "https://content1.rozetka.com.ua/goods/images/big/434340428.jpg"
                },
                new CategoryItemViewModel
                {
                    Id = 2,
                    Name = "Intel EVO",
                    Image = "https://content1.rozetka.com.ua/goods/images/big/368124548.jpg"
                },
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(_categories);
        } 
    }
}
