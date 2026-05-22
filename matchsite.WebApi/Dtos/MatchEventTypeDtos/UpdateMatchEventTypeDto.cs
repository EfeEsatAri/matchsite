namespace matchsite.WebApi.Dtos.MatchEventTypeDtos
{
    public class UpdateMatchEventTypeDto
    {
        public int MatchEventTypeId { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Color { get; set; }
    }
}
