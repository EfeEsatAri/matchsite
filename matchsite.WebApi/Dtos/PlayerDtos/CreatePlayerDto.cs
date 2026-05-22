namespace matchsite.WebApi.Dtos.PlayerDtos
{
    public class CreatePlayerDto
    {
        public string FullName { get; set; }
        public int Number { get; set; }
        public string Position { get; set; }
        public int TeamId { get; set; }
    }
}
