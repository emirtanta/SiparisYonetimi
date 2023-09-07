using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IService<Customer> _customerService;

        public CustomersController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customerService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Customer> Get(int id)
        {
            return await _customerService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer entity)
        {
            await _customerService.AddAsync(entity);

            await _customerService.SaveChangesAsync();

            return Ok($"Kayıt Başarıyla Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer entity)
        {
            _customerService.Update(entity);

            var result=await _customerService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Name} {entity.Surname} Adlı kayıt Başarıyla Güncellendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _customerService.FindAsync(id);

            if (record==null)
            {
                return BadRequest();
            }

            _customerService.Delete(record);    

            var result=await _customerService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"Kayıt Başarılı Bir Şekilde Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
