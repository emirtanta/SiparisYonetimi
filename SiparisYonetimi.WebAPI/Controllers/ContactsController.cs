using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;
using SiparisYonetimi.Service.Concrete;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IService<Contact> _contactService;

        public ContactsController(IService<Contact> contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            return await _contactService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact entity)
        {
            await _contactService.AddAsync(entity);

            await _contactService.SaveChangesAsync();

            return Ok($"Kayıt Başarıyla Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Contact entity)
        {
            _contactService.Update(entity);

            var result=await _contactService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Id} Numaralı Kayıt Başarılı Bir Şekilde Güncellendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _contactService.FindAsync(id);

            if (record == null)
            {
                return BadRequest();
            }
            _contactService.Delete(record);

            var result = await _contactService.SaveChangesAsync();

            if (result > 0)
            {
                return Ok("Kayıt Başarılı Bir Şekilde Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
