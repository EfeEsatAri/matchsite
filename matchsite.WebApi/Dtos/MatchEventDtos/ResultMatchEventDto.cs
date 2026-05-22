namespace matchsite.WebApi.Dtos.MatchEventDtos
{
    public class ResultMatchEventDto
    {
        public int MatchEventId { get; set; }
        public int Minute { get; set; }
        public int? ExtraTime { get; set; }
        public string TeamName { get; set; }
        public string PlayerFullName { get; set; }
        public string RelatedPlayerFullName { get; set; } 
        public string EventTypeName { get; set; } 
        public string Description { get; set; }
        public List<ResultMatchEventDto> MatchEvent { get; set; } = new List<ResultMatchEventDto>();
    }
}
