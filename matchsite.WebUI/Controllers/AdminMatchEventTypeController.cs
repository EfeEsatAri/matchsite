using matchsite.WebUI.Dtos.MatchEventTypeDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace matchsite.WebUI.Controllers
{
    public class AdminMatchEventTypeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMatchEventTypeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ETKİNLİK TÜRLERİNİ LİSTELEME (GET)
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7026/api/MatchEventTypes");
            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMatchEventTypeDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultMatchEventTypeDto>());
        }

        // YENİ ETKİNLİK TÜRÜ EKLEME (GET)
        [HttpGet]
        public IActionResult CreateMatchEventType()
        {
            return View();
        }

        // YENİ ETKİNLİK TÜRÜ EKLEME (POST)
        [HttpPost]
        public async Task<IActionResult> CreateMatchEventType(CreateMatchEventTypeDto createDto)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7026/api/MatchEventTypes");
            var jsonData = JsonConvert.SerializeObject(createDto);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // ETKİNLİK TÜRÜ SİLME (DELETE)
        public async Task<IActionResult> DeleteMatchEventType(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7026/api/MatchEventTypes/{id}");
            var responseMessage = await client.SendAsync(request);

            return RedirectToAction("Index");
        }

        // ETKİNLİK TÜRÜ GÜNCELLEME (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateMatchEventType(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7026/api/MatchEventTypes/{id}");
            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // ÇÖZÜM: Büyük/küçük harf duyarlılığını ortadan kaldıran ayar ekliyoruz
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

                var value = JsonConvert.DeserializeObject<UpdateMatchEventTypeDto>(jsonData, settings);

                // Güvenlik önlemi olarak kalabilir
                if (value != null && value.MatchEventTypeId == 0)
                {
                    value.MatchEventTypeId = id;
                }

                return View(value);
            }
            return RedirectToAction("Index");
        }

        // ETKİNLİK TÜRÜ GÜNCELLEME (POST/PUT)
        [HttpPost]
        public async Task<IActionResult> UpdateMatchEventType(UpdateMatchEventTypeDto updateDto)
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7026/api/MatchEventTypes/{updateDto.MatchEventTypeId}"); 
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
