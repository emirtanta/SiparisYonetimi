using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;
using System.Collections;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IService<Log> _logService;

        public LogsController(IService<Log> logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            return await _logService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Log> GetAsync(int id)
        {
            return await _logService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Log value)
        {
            value.CreateDate = DateTime.Now;

            await _logService.AddAsync(value);

            await _logService.SaveChangesAsync();

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Log value)
        {
            value.CreateDate = DateTime.Now;

            _logService.Update(value);

            await _logService.SaveChangesAsync();

            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logService.Delete(await _logService.FindAsync(id));

            await _logService.SaveChangesAsync();

            return Ok(); 
        }
    }
}
