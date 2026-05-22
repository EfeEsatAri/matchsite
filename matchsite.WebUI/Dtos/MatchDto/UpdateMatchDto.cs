namespace matchsite.WebUI.Dtos.MatchDto

{
    public class UpdateMatchDto
    {
        public int MatchId { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public bool IsFinished { get; set; }
        public string? MatchStatus { get; set; }
        public int TotalGoals { get; set; }
        public int TotalYellowCards { get; set; }
        public int TotalRedCards { get; set; }
    }
}
