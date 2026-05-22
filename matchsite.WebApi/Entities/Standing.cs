namespace matchsite.WebApi.Entities
{
    public class Standing
    {
        public int StandingId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int Played { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public string LastFiveMatches { get; set; } // Örn: "W|D|L|W|W"
        public int Position { get; set; }
    }
}
