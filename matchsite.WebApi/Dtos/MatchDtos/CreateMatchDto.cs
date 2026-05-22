namespace matchsite.WebApi.Dtos.MatchDtos
{
    public class CreateMatchDto
    {
        public DateTime MatchDate { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public string? MatchStatus { get; set; } = "NS"; // Not Started
    }
}
