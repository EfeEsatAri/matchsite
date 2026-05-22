using matchsite.WebApi.Dtos.MatchDtos;
using matchsite.WebApi.Dtos.MatchEventDtos;
using matchsite.WebApi.Entities;
using matchsite.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchService _matchService;
        private readonly MatchEventService _matchEventService;

        public MatchesController(MatchService matchService, MatchEventService matchEventService)
        {
            _matchService = matchService;
            _matchEventService = matchEventService;
        }

        [HttpGet]
        public IActionResult GetMatches()
        {
            var values = _matchService.GetAllMatches();
            var result = values.Select(x => new ResultMatchDto
            {
                MatchId = x.MatchId,
                MatchDate = x.MatchDate,
                HomeTeamName = x.HomeTeam.Name,
                HomeTeamLogo = x.HomeTeam.LogoUrl,
                AwayTeamName = x.AwayTeam.Name,
                AwayTeamLogo = x.AwayTeam.LogoUrl,
                HomeScore = x.HomeScore,
                AwayScore = x.AwayScore,
                IsFinished = x.IsFinished,
                MatchStatus = x.MatchStatus,
                TotalGoals = x.TotalGoals
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetMatchById(int id)
        {
            var match = _matchService.GetMatchById(id);
            if (match == null) return NotFound("Maç bulunamadı.");

            var events = _matchEventService.GetEventsByMatchId(id);

            var result = new ResultMatchDto
            {
                MatchId = match.MatchId,
                MatchDate = match.MatchDate,
                MatchStatus = match.MatchStatus,
                IsFinished = match.IsFinished,
                HomeScore = match.HomeScore,
                AwayScore = match.AwayScore,
                HomeTeamName = match.HomeTeam?.Name,
                HomeTeamLogo = match.HomeTeam?.LogoUrl,
                AwayTeamName = match.AwayTeam?.Name,
                AwayTeamLogo = match.AwayTeam?.LogoUrl,

                MatchEvent = events?.Select(e => new ResultMatchEventDto
                {
                    MatchEventId = e.MatchEventId,
                    Minute = e.Minute,
                    ExtraTime = e.ExtraTime,
                    TeamName = e.Team != null ? e.Team.Name : string.Empty,
                    PlayerFullName = e.Player != null ? e.Player.FullName : string.Empty,
                    RelatedPlayerFullName = e.RelatedPlayer != null ? e.RelatedPlayer.FullName : string.Empty,
                    EventTypeName = e.MatchEventType != null ? e.MatchEventType.Name : string.Empty,

                    Description = e.Description
                }).OrderBy(e => e.Minute).ToList() ?? new List<ResultMatchEventDto>()
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateMatch(CreateMatchDto dto)
        {
            var match = new Match
            {
                MatchDate = dto.MatchDate,
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                MatchStatus = dto.MatchStatus,
                IsFinished = false
            };
            _matchService.CreateMatch(match);
            return Ok("Maç eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMatch(int id, UpdateMatchDto dto)
        {
            var value = _matchService.GetMatchById(id);
            if (value == null)
            {
                return NotFound("Güncellenmek istenen maç bulunamadı.");
            }

            value.HomeScore = dto.HomeScore;
            value.AwayScore = dto.AwayScore;
            value.IsFinished = dto.IsFinished;
            value.MatchStatus = dto.MatchStatus;
            value.TotalGoals = dto.TotalGoals;
            value.TotalYellowCards = dto.TotalYellowCards;
            value.TotalRedCards = dto.TotalRedCards;

            try
            {
                _matchService.UpdateMatch(value);
                return Ok("Maç başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Güncelleme sırasında hata oluştu: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMatch(int id)
        {
            _matchService.DeleteMatch(id);
            return Ok("Maç silindi.");
        }
    }
}
