using matchsite.WebApi.Dtos.StandingDtos;
using matchsite.WebApi.Entities;
using matchsite.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly StandingService _service;
        public StandingsController(StandingService service) => _service = service;

        [HttpGet]
        public IActionResult GetList()
        {
            var values = _service.GetAll();
            var result = values.Select(x => new ResultStandingDto
            {
                StandingId = x.StandingId,
                Position = x.Position,
                TeamName = x.Team?.Name,
                LogoUrl = x.Team?.LogoUrl,
                Played = x.Played,
                Won = x.Won,
                Draw = x.Draw,
                Lost = x.Lost,
                GoalsFor = x.GoalsFor,
                GoalsAgainst = x.GoalsAgainst,
                GoalDifference = x.GoalDifference,
                Points = x.Points,
                LastFiveMatches = x.LastFiveMatches
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateStandingDto dto)
        {
            var s = new Standing
            {
                TeamId = dto.TeamId,
                Position = dto.Position,
                Played = dto.Played,
                Won = dto.Won,
                Draw = dto.Draw,
                Lost = dto.Lost,
                GoalsFor = dto.GoalsFor,
                GoalsAgainst = dto.GoalsAgainst,
                GoalDifference = dto.GoalDifference,
                Points = dto.Points,
                LastFiveMatches = dto.LastFiveMatches
            };
            _service.Create(s);
            return Ok("Puan durumu kaydı oluşturuldu.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,UpdateStandingDto dto)
        {
            var value = _service.GetById(dto.StandingId);
            if (value == null) return NotFound();

            value.TeamId = dto.TeamId;
            value.Position = dto.Position;
            value.Played = dto.Played;
            value.Won = dto.Won;
            value.Draw = dto.Draw;
            value.Lost = dto.Lost;
            value.GoalsFor = dto.GoalsFor;
            value.GoalsAgainst = dto.GoalsAgainst;
            value.GoalDifference = dto.GoalDifference;
            value.Points = dto.Points;
            value.LastFiveMatches = dto.LastFiveMatches;

            _service.Update(value);
            return Ok("Puan durumu güncellendi.");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var value = _service.GetById(id);
            if (value == null)
            {
                return NotFound("Silinmek istenen puan durumu kaydı bulunamadı.");
            }
            _service.Delete(id);

            return Ok("Puan durumu kaydı başarıyla silindi.");
        }
        [HttpGet("{id}")]
        public IActionResult GetStandingById(int id)
        {
            var standing = _service.GetById(id);

            if (standing == null)
            {
                return NotFound("Puan durumu kaydı bulunamadı.");
            }
            var dto = new UpdateStandingDto
            {
                StandingId = standing.StandingId,
                TeamId = standing.TeamId,
                Played = standing.Played,
                Won = standing.Won,
                Draw = standing.Draw,
                Lost = standing.Lost,
                GoalsFor = standing.GoalsFor,
                GoalsAgainst = standing.GoalsAgainst,
                GoalDifference = standing.GoalDifference,
                Points = standing.Points,
                LastFiveMatches = standing.LastFiveMatches,
                Position = standing.Position
            };
            return Ok(dto);
        }
    }
}
