using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IService<Product> _service;
        private readonly IProductService _productService;

        public ProductsController(IService<Product> service, IProductService productService)
        {
            _service = service;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productService.GetProductsByCategoryAndBrandAsync();
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await _service.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product entity)
        {
            await _service.AddAsync(entity);

            await _service.SaveChangesAsync();

            return Ok($"{entity.Name} Adlı Ürün Başarılı Bir Şekilde Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product entity)
        {
            _service.Update(entity);

            var result = await _service.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Id} Numaralı Kayıt Başarılı Bir Şekilde Eklendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _service.FindAsync(id);

            if (record==null)
            {
                return BadRequest();
            }

            _service.Delete(record);

            var result=await _service.SaveChangesAsync();

            if (result>0) 
            {
                return Ok($"Kayıt Başarılı Bir Şekilde Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
