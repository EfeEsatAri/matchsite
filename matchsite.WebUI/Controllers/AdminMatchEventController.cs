using matchsite.WebUI.Dtos.MatchEventDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace matchsite.WebUI.Controllers
{
    public class AdminMatchEventController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMatchEventController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ETKİNLİKLERİ LİSTELEME (GET)
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/MatchEvents");
            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMatchEventDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultMatchEventDto>());
        }

        // YENİ ETKİNLİK EKLEME (GET)
        [HttpGet]
        public async Task<IActionResult> CreateMatchEvent()
        {
            var client = _httpClientFactory.CreateClient();

            // Formda seçtireceğimiz tüm ilişkili tabloları çekiyoruz
            var matchResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Matches"));
            var teamResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Teams"));
            var playerResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Players"));
            var typeResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/MatchEventTypes"));

            if (matchResponse.IsSuccessStatusCode && teamResponse.IsSuccessStatusCode &&
                playerResponse.IsSuccessStatusCode && typeResponse.IsSuccessStatusCode)
            {
                ViewBag.Matches = JsonConvert.DeserializeObject<List<dynamic>>(await matchResponse.Content.ReadAsStringAsync());
                ViewBag.Teams = JsonConvert.DeserializeObject<List<dynamic>>(await teamResponse.Content.ReadAsStringAsync());
                ViewBag.Players = JsonConvert.DeserializeObject<List<dynamic>>(await playerResponse.Content.ReadAsStringAsync());
                ViewBag.EventTypes = JsonConvert.DeserializeObject<List<dynamic>>(await typeResponse.Content.ReadAsStringAsync());
            }

            return View();
        }

        // YENİ ETKİNLİK EKLEME (POST)s
        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent(CreateMatchEventDto createDto)
        {
            if (createDto.PlayerId == 0) createDto.PlayerId = null;
            if (createDto.RelatedPlayerId == 0) createDto.RelatedPlayerId = null;
            if (createDto.ExtraTime == 0) createDto.ExtraTime = null;

            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7026/api/MatchEvents");
            var jsonData = JsonConvert.SerializeObject(createDto);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            await PopulateViewBags(client);

            return View(createDto);
        }

        private async Task PopulateViewBags(HttpClient client)
        {
            var matchResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Matches"));
            var teamResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Teams"));
            var playerResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Players"));
            var typeResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/MatchEventTypes"));

            if (matchResponse.IsSuccessStatusCode) ViewBag.Matches = JsonConvert.DeserializeObject<List<dynamic>>(await matchResponse.Content.ReadAsStringAsync());
            if (teamResponse.IsSuccessStatusCode) ViewBag.Teams = JsonConvert.DeserializeObject<List<dynamic>>(await teamResponse.Content.ReadAsStringAsync());
            if (playerResponse.IsSuccessStatusCode) ViewBag.Players = JsonConvert.DeserializeObject<List<dynamic>>(await playerResponse.Content.ReadAsStringAsync());
            if (typeResponse.IsSuccessStatusCode) ViewBag.EventTypes = JsonConvert.DeserializeObject<List<dynamic>>(await typeResponse.Content.ReadAsStringAsync());
        }

        // ETKİNLİK SİLME (DELETE)
        public async Task<IActionResult> DeleteMatchEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7026/api/MatchEvents/{id}");
            var responseMessage = await client.SendAsync(request);

            return RedirectToAction("Index");
        }

        // ETKİNLİK GÜNCELLEME (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateMatchEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // 1. Mevcut Etkinlik Verisi
            var eventRequest = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7026/api/MatchEvents/{id}");
            var eventResponse = await client.SendAsync(eventRequest);

            // 2. Açılır Kutular (SelectBox) İçin Gerekli İlişkisel Listeler
            var matchResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Matches"));
            var teamResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Teams"));
            var playerResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/Players"));
            var typeResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/MatchEventTypes"));

            if (eventResponse.IsSuccessStatusCode)
            {
                ViewBag.Matches = JsonConvert.DeserializeObject<List<dynamic>>(await matchResponse.Content.ReadAsStringAsync());
                ViewBag.Teams = JsonConvert.DeserializeObject<List<dynamic>>(await teamResponse.Content.ReadAsStringAsync());
                ViewBag.Players = JsonConvert.DeserializeObject<List<dynamic>>(await playerResponse.Content.ReadAsStringAsync());
                ViewBag.EventTypes = JsonConvert.DeserializeObject<List<dynamic>>(await typeResponse.Content.ReadAsStringAsync());

                var jsonData = await eventResponse.Content.ReadAsStringAsync();

                // Harf uyuşmazlığına takılmamak için CamelCase ayarı ekliyoruz
                var settings = new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };
                var value = JsonConvert.DeserializeObject<UpdateMatchEventDto>(jsonData, settings);

                if (value != null && value.MatchEventId == 0) value.MatchEventId = id;

                return View(value);
            }
            return RedirectToAction("Index");
        }

        // ETKİNLİK GÜNCELLEME (POST/PUT)
        [HttpPost]
        public async Task<IActionResult> UpdateMatchEvent(UpdateMatchEventDto updateDto)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7026/api/MatchEvents/{updateDto.MatchEventId}");
            var jsonData = JsonConvert.SerializeObject(updateDto);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(updateDto);
        }
    }
}
