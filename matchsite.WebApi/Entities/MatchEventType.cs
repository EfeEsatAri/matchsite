namespace matchsite.WebApi.Entities
{
    public class MatchEventType
    {
        public int MatchEventTypeId { get; set; }
        public string Name { get; set; } // Gol, Kart, Değişiklik
        public string IconUrl { get; set; }
        public string Color { get; set; }
        public ICollection<MatchEvent> MatchEvents { get; set; }
    }
}
