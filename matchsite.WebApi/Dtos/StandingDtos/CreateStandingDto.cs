namespace matchsite.WebApi.Dtos.StandingDtos
{
    public class CreateStandingDto
    {
        public int TeamId { get; set; }
        public int Position { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public string LastFiveMatches { get; set; }
    }
}
