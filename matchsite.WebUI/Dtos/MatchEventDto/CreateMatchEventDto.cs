namespace matchsite.WebUI.Dtos.MatchEventDto

{
    public class CreateMatchEventDto
    {
        public int MatchId { get; set; }
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public int? RelatedPlayerId { get; set; }
        public int MatchEventTypeId { get; set; }
        public int Minute { get; set; }
        public int? ExtraTime { get; set; }
        public string Description { get; set; }
    }
}
