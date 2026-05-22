using matchsite.WebUI.Dtos.MatchEventDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace matchsite.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Dashboard()
        {
            var client = _httpClientFactory.CreateClient();

            var teamRes = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Teams"));
            var playerRes = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Players"));
            var matchRes = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Matches"));
            var eventRes = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/MatchEvents"));

            int teamCount = 0, playerCount = 0, matchCount = 0, eventCount = 0;
            var latestEvents = new List<ResultMatchEventDto>(); // Son olayları tutacak liste

            if (teamRes.IsSuccessStatusCode)
                teamCount = JsonConvert.DeserializeObject<List<dynamic>>(await teamRes.Content.ReadAsStringAsync())?.Count ?? 0;

            if (playerRes.IsSuccessStatusCode)
                playerCount = JsonConvert.DeserializeObject<List<dynamic>>(await playerRes.Content.ReadAsStringAsync())?.Count ?? 0;

            if (matchRes.IsSuccessStatusCode)
                matchCount = JsonConvert.DeserializeObject<List<dynamic>>(await matchRes.Content.ReadAsStringAsync())?.Count ?? 0;

            if (eventRes.IsSuccessStatusCode)
            {
                var eventJson = await eventRes.Content.ReadAsStringAsync();
                var allEvents = JsonConvert.DeserializeObject<List<ResultMatchEventDto>>(eventJson);

                if (allEvents != null)
                {
                    eventCount = allEvents.Count;

                    latestEvents = allEvents.Take(4).ToList();
                }
            }

            // Sayaçlar
            ViewBag.TeamCount = teamCount;
            ViewBag.PlayerCount = playerCount;
            ViewBag.MatchCount = matchCount;
            ViewBag.EventCount = eventCount;

            // Son Canlı Olaylar Listesi
            ViewBag.LatestEvents = latestEvents;

            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
