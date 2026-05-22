namespace matchsite.WebApi.Controllers
{
    internal class MatchEventDto
    {
        public int MatchEventId { get; set; }
        public int Minute { get; set; }
        public int? ExtraTime { get; set; }
        public object TeamName { get; set; }
        public object PlayerFullName { get; set; }
        public object RelatedPlayerFullName { get; set; }
        public object EventTypeName { get; set; }
        public string Description { get; set; }
    }
}