using matchsite.WebApi.Context;
using matchsite.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace matchsite.WebApi.Services
{
    public class MatchService
    {
        private readonly ApiContext _context;

        public MatchService(ApiContext context)
        {
            _context = context;
        }

        public List<Match> GetAllMatches() =>
            _context.Matches.Include(x => x.HomeTeam).Include(x => x.AwayTeam).ToList();

        public Match GetMatchById(int id) =>
            _context.Matches.Include(x => x.HomeTeam).Include(x => x.AwayTeam).FirstOrDefault(x => x.MatchId == id);

        public void CreateMatch(Match match) { _context.Matches.Add(match); _context.SaveChanges(); }
        public void UpdateMatch(Match match) { _context.Matches.Update(match); _context.SaveChanges(); }
        public void DeleteMatch(int id)
        {
            var match = _context.Matches.Find(id);
            if (match != null) { _context.Matches.Remove(match); _context.SaveChanges(); }
        }
    }
}
