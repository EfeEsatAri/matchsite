using matchsite.WebApi.Context;
using matchsite.WebApi.Dtos.MatchEventDtos;
using matchsite.WebApi.Entities;
using matchsite.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace matchsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchEventsController : ControllerBase
    {
        private readonly MatchEventService _eventService;
        private readonly ApiContext _context; 

        public MatchEventsController(MatchEventService eventService, ApiContext context)
        {
            _eventService = eventService;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var values = _context.MatchEvents
                .Include(x => x.Team)
                .Include(x => x.Player)
                .Include(x => x.RelatedPlayer)
                .Include(x => x.MatchEventType)
                .OrderByDescending(x => x.MatchEventId) 
                .ToList();

            var result = values.Select(x => new ResultMatchEventDto
            {
                MatchEventId = x.MatchEventId,
                Minute = x.Minute,
                ExtraTime = x.ExtraTime,
                TeamName = x.Team?.Name ?? "N/A",
                PlayerFullName = x.Player?.FullName ?? "N/A",
                RelatedPlayerFullName = x.RelatedPlayer?.FullName ?? "",

                EventTypeName = x.MatchEventType?.Name ?? "N/A",
                Description = x.Description
            }).ToList();

            return Ok(result);
        }

        [HttpGet("ByMatch/{matchId}")]
        public IActionResult GetEventsByMatch(int matchId)
        {
            var values = _eventService.GetEventsByMatchId(matchId);
            var result = values.Select(x => new ResultMatchEventDto
            {
                MatchEventId = x.MatchEventId,
                Minute = x.Minute,
                ExtraTime = x.ExtraTime,
                TeamName = x.Team?.Name ?? "N/A",

                PlayerFullName = x.Player?.FullName ?? "N/A",
                RelatedPlayerFullName = x.RelatedPlayer?.FullName ?? "",

                EventTypeName = x.MatchEventType?.Name ?? "N/A",
                Description = x.Description
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateEvent(CreateMatchEventDto dto)
        {
            var matchEvent = new MatchEvent
            {
                MatchId = dto.MatchId,
                TeamId = dto.TeamId,
                // Eğer UI'dan 0 seçeneği geldiyse veritabanına null bassın (İlişki hatası vermesin)
                PlayerId = dto.PlayerId == 0 ? null : dto.PlayerId,
                RelatedPlayerId = dto.RelatedPlayerId == 0 ? null : dto.RelatedPlayerId,
                MatchEventTypeId = dto.MatchEventTypeId,
                Minute = dto.Minute,
                ExtraTime = dto.ExtraTime,
                Description = dto.Description
            };
            _eventService.Create(matchEvent);
            return Ok("Maç olayı kaydedildi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var matchEvent = _eventService.GetById(id);
            if (matchEvent == null) return NotFound("Etkinlik bulunamadı.");

            var dto = new UpdateMatchEventDto
            {
                MatchEventId = matchEvent.MatchEventId,
                MatchId = matchEvent.MatchId,
                TeamId = matchEvent.TeamId,
                PlayerId = matchEvent.PlayerId,
                RelatedPlayerId = matchEvent.RelatedPlayerId,
                MatchEventTypeId = matchEvent.MatchEventTypeId,
                Minute = matchEvent.Minute,
                ExtraTime = matchEvent.ExtraTime,
                Description = matchEvent.Description
            };
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateMatchEventDto dto)
        {
            var value = _eventService.GetById(id);
            if (value == null) return NotFound();

            value.MatchId = dto.MatchId;
            value.TeamId = dto.TeamId;
            value.PlayerId = dto.PlayerId == 0 ? null : dto.PlayerId;
            value.RelatedPlayerId = dto.RelatedPlayerId == 0 ? null : dto.RelatedPlayerId;
            value.MatchEventTypeId = dto.MatchEventTypeId;
            value.Minute = dto.Minute;
            value.ExtraTime = dto.ExtraTime;
            value.Description = dto.Description;

            _eventService.Update(value);
            return Ok("Maç etkinliği güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            _eventService.Delete(id);
            return Ok("Olay silindi.");
        }
    }
}
