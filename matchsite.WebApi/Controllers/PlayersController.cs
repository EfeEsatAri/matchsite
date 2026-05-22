using matchsite.WebApi.Dtos.PlayerDtos;
using matchsite.WebApi.Entities;
using matchsite.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace matchsite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;
        public PlayersController(PlayerService playerService) => _playerService = playerService;

        [HttpGet]
        public IActionResult GetPlayers()
        {
            var values = _playerService.GetAllPlayers();
            var result = values.Select(x => new ResultPlayerDto
            {
                PlayerId = x.PlayerId,
                FullName = x.FullName,
                Number = x.Number,     
                Position = x.Position,
                TeamName = x.Team.Name
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreatePlayer(CreatePlayerDto dto)
        {
            var player = new Player
            {
                FullName = dto.FullName,
                Number = dto.Number,
                Position = dto.Position,
                TeamId = dto.TeamId
            };
            _playerService.CreatePlayer(player);
            return Ok("Oyuncu başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, UpdatePlayerDto dto)
        {
            var existingPlayer = _playerService.GetPlayerById(id);
            if (existingPlayer == null) return NotFound("Oyuncu bulunamadı.");

            existingPlayer.FullName = dto.FullName;
            existingPlayer.Number = dto.Number;
            existingPlayer.Position = dto.Position;
            existingPlayer.TeamId = dto.TeamId;

            _playerService.UpdatePlayer(existingPlayer);
            return Ok("Oyuncu bilgileri güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            _playerService.DeletePlayer(id);
            return Ok("Oyuncu silindi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
            {
                return NotFound("Oyuncu bulunamadı.");
            }
            var dto = new UpdatePlayerDto
            {
                PlayerId = player.PlayerId,
                FullName = player.FullName,
                Number = player.Number,
                Position = player.Position,
                TeamId = player.TeamId
            };

            return Ok(dto);
        }
    }
}
