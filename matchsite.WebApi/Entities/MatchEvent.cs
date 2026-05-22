namespace matchsite.WebApi.Entities
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }
        public string? ExternalEventId { get; set; } // API'den gelen olay ID'si

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        // Oyuncu bazlı olmayan olaylar için null olabilir
        public int? PlayerId { get; set; }
        public Player Player { get; set; }

        // Asist yapan veya Giren oyuncu için
        public int? RelatedPlayerId { get; set; }
        public Player RelatedPlayer { get; set; }

        public int MatchEventTypeId { get; set; }
        public MatchEventType MatchEventType { get; set; }

        public int Minute { get; set; }
        public int? ExtraTime { get; set; } // 90+3 gibi durumlar
        public string Description { get; set; }
    }
}
