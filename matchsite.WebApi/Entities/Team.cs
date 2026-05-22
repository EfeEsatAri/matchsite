using System.Numerics;
using System.Text.RegularExpressions;

namespace matchsite.WebApi.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string? ExternalId { get; set; } // API'den gelen ID
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }
        public string Stadium { get; set; }
        public string Coach { get; set; }

        // İlişkiler
        public ICollection<Match> HomeMatches { get; set; }
        public ICollection<Match> AwayMatches { get; set; }
        public ICollection<Player> Players { get; set; }
        public Standing Standing { get; set; }
    }
}
