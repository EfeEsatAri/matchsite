using matchsite.WebApi.Context;
using matchsite.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace matchsite.WebApi.Services
{
    public class MatchEventService
    {
        private readonly ApiContext _context;
        public MatchEventService(ApiContext context) => _context = context;

        public List<MatchEvent> GetEventsByMatchId(int matchId)
        {
            return _context.MatchEvents
                .Include(x => x.Team)
                .Include(x => x.Player)
                .Include(x => x.RelatedPlayer)
                .Include(x => x.MatchEventType)
                .Where(x => x.MatchId == matchId)
                .OrderBy(x => x.Minute)
                .ToList();
        }

        public MatchEvent GetById(int id) => _context.MatchEvents.Find(id);

        public void Create(MatchEvent matchEvent)
        {
            _context.MatchEvents.Add(matchEvent);
            _context.SaveChanges();
        }

        public void Update(MatchEvent matchEvent) => _context.SaveChanges();

        public void Delete(int id)
        {
            var value = _context.MatchEvents.Find(id);
            if (value != null) { _context.MatchEvents.Remove(value); _context.SaveChanges(); }
        }
    }
}
