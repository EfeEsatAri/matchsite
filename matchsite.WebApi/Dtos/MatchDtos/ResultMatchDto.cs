using matchsite.WebApi.Dtos.MatchEventDtos;

namespace matchsite.WebApi.Dtos.MatchDtos
{
    public class ResultMatchDto
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamLogo { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public bool IsFinished { get; set; }
        public string? MatchStatus { get; set; }
        public int TotalGoals { get; set; }
        public List<ResultMatchEventDto>? MatchEvent { get; internal set; }
    }
}
