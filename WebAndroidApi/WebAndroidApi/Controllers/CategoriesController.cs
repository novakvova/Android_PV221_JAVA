using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAndroidApi.Data;
using WebAndroidApi.Models.Category;

namespace WebAndroidApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly MyStoreContext _context;
        public readonly IMapper _mapper;

        public CategoriesController(MyStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var model = await _context.Categories
                .ProjectTo<CategoryItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(model);
        } 
    }
}
