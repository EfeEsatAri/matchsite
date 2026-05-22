using matchsite.WebApi.Context;
using matchsite.WebApi.Entities;

namespace matchsite.WebApi.Services
{
    public class MatchEventTypeService
    {
        private readonly ApiContext _context;
        public MatchEventTypeService(ApiContext context) => _context = context;

        public List<MatchEventType> GetAll() => _context.MatchEventTypes.ToList();

        public MatchEventType GetById(int id) => _context.MatchEventTypes.Find(id);

        public void Create(MatchEventType type)
        {
            _context.MatchEventTypes.Add(type);
            _context.SaveChanges();
        }

        public void Update(MatchEventType type) => _context.SaveChanges();

        public void Delete(int id)
        {
            var value = _context.MatchEventTypes.Find(id);
            if (value != null) { _context.MatchEventTypes.Remove(value); _context.SaveChanges(); }
        }
    }
}
