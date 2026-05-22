using matchsite.WebApi.Context;
using matchsite.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace matchsite.WebApi.Services
{
    public class StandingService
    {
        private readonly ApiContext _context;
        public StandingService(ApiContext context) => _context = context;

        public List<Standing> GetAll()
        {
            return _context.Standings
                .Include(x => x.Team)
                .OrderByDescending(x => x.Points) // Önce Puan
                .ThenByDescending(x => (x.GoalsFor - x.GoalsAgainst)) // Sonra Averaj
                .ToList();
        }

        public Standing GetById(int id) => _context.Standings.Find(id);

        public void Create(Standing standing) { _context.Standings.Add(standing); _context.SaveChanges(); }
        public void Update(Standing standing) => _context.SaveChanges();
        public void Delete(int id)
        {
            var value = _context.Standings.Find(id);
            if (value != null) { _context.Standings.Remove(value); _context.SaveChanges(); }
        }
    }
}
