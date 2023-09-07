using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(int id)
        {
            return await _categoryService.GetCategoryByProducts(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category entity)
        {
            await _categoryService.AddAsync(entity);

            await _categoryService.SaveChangesAsync();

            return Ok("Kategori Başarılı Bir Şekilde Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Category entity)
        {
            _categoryService.Update(entity);

            var result=await _categoryService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Name} Adlı Kategori Başarılı Bir Şekilde Güncellendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record=await _categoryService.FindAsync(id);

            if (record==null)
            {
                return BadRequest();
            }

            _categoryService.Delete(record);

            var result=await _categoryService.SaveChangesAsync();

            if (result>0)
            {
                return Ok("Kayıt Başarılı Bir Şekilde Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
