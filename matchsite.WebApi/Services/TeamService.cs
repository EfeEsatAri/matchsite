using matchsite.WebApi.Context;
using matchsite.WebApi.Entities;

namespace matchsite.WebApi.Services
{
    public class TeamService
    {
        private readonly ApiContext _context;

        public TeamService(ApiContext context)
        {
            _context = context;
        }
        public List<Team> GetAllTeams() => _context.Teams.ToList();

        public Team GetTeamById(int id) => _context.Teams.Find(id);

        public void CreateTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }

        public void DeleteTeam(int id)
        {
            var team = _context.Teams.Find(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
        }
    }
}
