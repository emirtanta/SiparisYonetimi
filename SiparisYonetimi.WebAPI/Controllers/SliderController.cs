using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly IService<Slide> _slideService;

        public SliderController(IService<Slide> slideService)
        {
            _slideService = slideService;
        }

        [HttpGet]
        public async Task<IEnumerable<Slide>> GetAll()
        {
            return await _slideService.GetAllAsync();

            
        }

        [HttpGet("{id}")]
        public async Task<Slide> Get(int id)
        {
            return await _slideService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Slide entity)
        {
            await _slideService.AddAsync(entity);

            await _slideService.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Slide entity)
        {
            _slideService.Update(entity);

            var result=await _slideService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Id} Numaralı Kayıt Başarıyla Güncellendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _slideService.FindAsync(id);

            if(record==null)
            {
                return BadRequest();
            }

            _slideService.Delete(record);

            var result= await _slideService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"Kayıt Başarıyla Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
