using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IService<User> _userService;

        public UsersController(IService<User> userService)
        {
            _userService = userService;
        }

        //GET:api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAllAsync();
        }

        //Get api/Users/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _userService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User entity)
        {
            await _userService.AddAsync(entity);

            await _userService.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]User entity)
        {
            _userService.Update(entity);

            var result = await _userService.SaveChangesAsync();

            if (result>0)
            {
                return Ok($"{entity.Name} {entity.Surname} adlu kullanıcı başarıyla güncellendi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _userService.FindAsync(id);

            if (record==null) 
            {
                return BadRequest();
            }

            _userService.Delete(record);

            var result=await _userService.SaveChangesAsync();

            if (result>0)
            {
                return Ok("Kayıt Başarıyla Silindi");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
}
