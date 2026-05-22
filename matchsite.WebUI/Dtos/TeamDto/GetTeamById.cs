namespace matchsite.WebUI.Dtos.TeamDto
{
    public class GetTeamById
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LogoUrl { get; set; }
        public string Stadium { get; set; }
        public string Coach { get; set; }
    }
}
