using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;
using System.Collections;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IService<Setting> _settingService;

        public SettingsController(IService<Setting> settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IEnumerable<Setting>> GetAll()
        {
            return await _settingService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Setting> GetAsync(int id)
        {
            return await _settingService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Setting value)
        {

            await _settingService.AddAsync(value);

            await _settingService.SaveChangesAsync();

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Setting value)
        {

            _settingService.Update(value);

            await _settingService.SaveChangesAsync();

            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _settingService.Delete(await _settingService.FindAsync(id));

            await _settingService.SaveChangesAsync();

            return Ok(); 
        }
    }
}
