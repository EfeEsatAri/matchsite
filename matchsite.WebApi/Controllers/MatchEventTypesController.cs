using matchsite.WebApi.Dtos.MatchEventTypeDtos;
using matchsite.WebApi.Entities;
using matchsite.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventTypesController : ControllerBase
    {
        private readonly MatchEventTypeService _service;
        public MatchEventTypesController(MatchEventTypeService service) => _service = service;

        [HttpGet]
        public IActionResult GetList()
        {
            var values = _service.GetAll();
            return Ok(values); 
        }

        [HttpPost]
        public IActionResult Create(CreateMatchEventTypeDto dto)
        {
            var type = new MatchEventType
            {
                Name = dto.Name,
                IconUrl = dto.IconUrl,
                Color = dto.Color
            };
            _service.Create(type);
            return Ok("Olay tipi başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateMatchEventTypeDto dto)
        {
            var value = _service.GetById(id);
            if (value == null) return NotFound("Güncellenmek istenen etkinlik türü bulunamadı.");

            value.Name = dto.Name;
            value.IconUrl = dto.IconUrl;
            value.Color = dto.Color;

            _service.Update(value);
            return Ok("Olay tipi güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Olay tipi silindi.");
        }
        [HttpGet("{id}")]
        public IActionResult GetMatchEventTypeById(int id)
        {
            var eventType = _service.GetById(id);
            if (eventType == null) return NotFound("Etkinlik türü bulunamadı.");

            var dto = new UpdateMatchEventTypeDto
            {
                MatchEventTypeId = eventType.MatchEventTypeId,
                Name = eventType.Name,
                IconUrl = eventType.IconUrl,
                Color = eventType.Color
            };
            return Ok(dto);
        }
    }
}
