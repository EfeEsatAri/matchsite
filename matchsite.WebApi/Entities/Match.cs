namespace matchsite.WebApi.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public string? ExternalId { get; set; } // API'den gelen ID
        public DateTime MatchDate { get; set; }

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        // Skorlar oynanmamış maçlar için null olabilmeli
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public bool IsFinished { get; set; }
        public string? MatchStatus { get; set; } // Örn: "H1", "FT", "P"

        // Genel İstatistikler
        public int TotalGoals { get; set; }
        public int TotalYellowCards { get; set; }
        public int TotalRedCards { get; set; }

        public ICollection<MatchEvent> MatchEvents { get; set; }
    }
}
