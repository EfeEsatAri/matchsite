using matchsite.WebApi.Dtos.TeamDtos;
using matchsite.WebApi.Entities;
using matchsite.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;
        private readonly MatchService _matchService;

        public TeamsController(TeamService teamService, MatchService matchService)
        {
            _teamService = teamService;
            _matchService = matchService;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var teams = _teamService.GetAllTeams();

            // Entity -> ResultTeamDto dönüşümü
            var result = teams.Select(x => new ResultTeamDto
            {
                TeamId = x.TeamId,
                Name = x.Name,
                ShortName = x.ShortName,
                LogoUrl = x.LogoUrl,
                Stadium = x.Stadium,
                Coach = x.Coach
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")] 
        public IActionResult GetTeamById(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
            {
                return NotFound("Takım bulunamadı.");
            }

            var result = new UpdateTeamDto
            {
                TeamId = team.TeamId,
                Name = team.Name,
                ShortName = team.ShortName,
                LogoUrl = team.LogoUrl,
                Stadium = team.Stadium,
                Coach = team.Coach
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateTeam(CreateTeamDto dto)
        {
            var team = new Team
            {
                Name = dto.Name,
                ShortName = dto.ShortName,
                LogoUrl = dto.LogoUrl,
                Stadium = dto.Stadium,
                Coach = dto.Coach,
                ExternalId = dto.ExternalId
            };

            _teamService.CreateTeam(team);
            return Ok("Takım başarıyla oluşturuldu.");
        }

        [HttpPut]
        public IActionResult UpdateTeam(UpdateTeamDto dto)
        {
            var existingTeam = _teamService.GetTeamById(dto.TeamId);
            if (existingTeam == null) return NotFound();

            existingTeam.Name = dto.Name;
            existingTeam.ShortName = dto.ShortName;
            existingTeam.LogoUrl = dto.LogoUrl;
            existingTeam.Stadium = dto.Stadium;
            existingTeam.Coach = dto.Coach;

            _teamService.UpdateTeam(existingTeam);
            return Ok("Güncellendi.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            // Önce silinecek takımı buluyoruz
            var value = _teamService.GetTeamById(id);

            if (value == null)
            {
                return NotFound("Silinmek istenen takım bulunamadı.");
            }

            // Servis üzerinden silme işlemini yapıyoruz
            _teamService.DeleteTeam(id);

            return Ok("Takım başarıyla silindi.");
        }
    }
}
