using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IService<Brand> _brandService;

        public BrandsController(IService<Brand> brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _brandService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Brand> Get(int id)
        {
            return await _brandService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Brand entity)
        {
            entity.CreateDate = DateTime.Now;

            await _brandService.AddAsync(entity);

            await _brandService.SaveChangesAsync();

            return Ok($"{entity.Name} Adlı Kayıt Başarıyla Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Brand entity)
        {
            entity.CreateDate = DateTime.Now;

            _brandService.Update(entity);

            var result=await _brandService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Id} Numaralı Marka Başarıyla Güncellendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _brandService.FindAsync(id);

            if(record==null)
            {
                return BadRequest();
            }

            _brandService.Delete(record);

            var result = await _brandService.SaveChangesAsync();

            if (result>0)
            {
                return Ok("Kayıt Başarılı Bir Şekilde Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
