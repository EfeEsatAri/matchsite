using matchsite.WebUI.Dtos.StandingDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace matchsite.WebUI.Controllers
{
    public class AdminStandingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminStandingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // PUAN DURUMU LİSTELEME (GET)
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            // Adresi doğrudan metodun içinde tanımladık
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Standings");
            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultStandingDto>>(jsonData);

                var orderedValues = values.OrderByDescending(x => x.Points)
                                          .ThenByDescending(x => x.GoalDifference)
                                          .ToList();

                return View(orderedValues);
            }
            return View(new List<ResultStandingDto>());
        }

        // YENİ PUAN DURUMU EKLEME (GET)
        [HttpGet]
        public async Task<IActionResult> CreateStanding()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Teams");
            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var teamJson = await responseMessage.Content.ReadAsStringAsync();
                ViewBag.Teams = JsonConvert.DeserializeObject<List<dynamic>>(teamJson);
            }
            return View();
        }

        // YENİ PUAN DURUMU EKLEME (POST)
        [HttpPost]
        public async Task<IActionResult> CreateStanding(CreateStandingDto createStandingDto)
        {
            createStandingDto.GoalDifference = createStandingDto.GoalsFor - createStandingDto.GoalsAgainst;

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7026/api/Standings");
            var jsonData = JsonConvert.SerializeObject(createStandingDto);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // PUAN DURUMU SİLME (DELETE)
        public async Task<IActionResult> DeleteStanding(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7026/api/Standings/{id}");
            var responseMessage = await client.SendAsync(request);

            return RedirectToAction("Index");
        }

        // PUAN DURUMU GÜNCELLEME (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateStanding(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var standingRequest = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7026/api/Standings/{id}");
            var standingResponse = await client.SendAsync(standingRequest);

            var teamRequest = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Teams");
            var teamResponse = await client.SendAsync(teamRequest);

            if (standingResponse.IsSuccessStatusCode && teamResponse.IsSuccessStatusCode)
            {
                var teamJson = await teamResponse.Content.ReadAsStringAsync();
                ViewBag.Teams = JsonConvert.DeserializeObject<List<dynamic>>(teamJson);

                var standingJson = await standingResponse.Content.ReadAsStringAsync();
                var standingData = JsonConvert.DeserializeObject<UpdateStandingDto>(standingJson);
                return View(standingData);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStanding(UpdateStandingDto updateStandingDto)
        {
            updateStandingDto.GoalDifference = updateStandingDto.GoalsFor - updateStandingDto.GoalsAgainst;

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7026/api/Standings/{updateStandingDto.StandingId}");
            var jsonData = JsonConvert.SerializeObject(updateStandingDto);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(updateStandingDto);
        }
    }
}
