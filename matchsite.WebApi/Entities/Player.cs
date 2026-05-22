namespace matchsite.WebApi.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string? ExternalId { get; set; }
        public string FullName { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public ICollection<MatchEvent> MatchEvents { get; set; }
    }
}
