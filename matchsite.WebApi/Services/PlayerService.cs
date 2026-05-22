using matchsite.WebApi.Context;
using matchsite.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace matchsite.WebApi.Services
{
    public class PlayerService
    {
        private readonly ApiContext _context;
        public PlayerService(ApiContext context) => _context = context;

        public List<Player> GetAllPlayers() =>
            _context.Players.Include(x => x.Team).ToList();

        public Player GetPlayerById(int id) =>
            _context.Players.Include(x => x.Team).FirstOrDefault(x => x.PlayerId == id);

        public void CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void UpdatePlayer(Player player)
        {
            _context.SaveChanges();
        }

        public void DeletePlayer(int id)
        {
            var player = _context.Players.Find(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                _context.SaveChanges();
            }
        }
    }
}
