using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;
using System.Collections;

namespace SiparisYonetimi.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IService<News> _newsService;

        public NewsController(IService<News> newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IEnumerable<News>> GetAll()
        {
            return await _newsService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<News> GetAsync(int id)
        {
            return await _newsService.FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] News value)
        {
            value.CreateDate = DateTime.Now;

            await _newsService.AddAsync(value);

            await _newsService.SaveChangesAsync();

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] News value)
        {
            value.CreateDate = DateTime.Now;

            _newsService.Update(value);

            await _newsService.SaveChangesAsync();

            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _newsService.Delete(await _newsService.FindAsync(id));

            await _newsService.SaveChangesAsync();

            return Ok(); 
        }
    }
}
