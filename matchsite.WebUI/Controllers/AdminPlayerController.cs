using matchsite.WebUI.Dtos.PlayerDto;
using matchsite.WebUI.Dtos.TeamDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace matchsite.WebUI.Controllers
{
    public class AdminPlayerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminPlayerController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 11;
            // Hata almamak için default değerleri en başta tanımlıyoruz
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = 1;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Players");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPlayerDto>>(jsonData);

                if (values != null && values.Any())
                {
                    var pagedData = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    ViewBag.TotalPages = (int)Math.Ceiling((double)values.Count / pageSize);
                    return View(pagedData);
                }
            }

            return View(new List<ResultPlayerDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreatePlayer()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Teams");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // Hata veren ResultTeamDto yerine doğrudan buraya bağla
                var teams = JsonConvert.DeserializeObject<List<ResultTeamDto>>(jsonData);

                ViewBag.Teams = teams;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createPlayerDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7026/api/Players", content);
            return RedirectToAction("Index");
        }

        [HttpGet] 
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.DeleteAsync($"https://localhost:7026/api/Players/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePlayer(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // 1. Aşama: Oyuncunun mevcut bilgilerini API'den çekiyoruz
            var playerResponse = await client.GetAsync($"https://localhost:7026/api/Players/{id}");

            // 2. Aşama: Takım listesini dinamik select için çekiyoruz
            var teamResponse = await client.GetAsync("https://localhost:7026/api/Teams");

            if (playerResponse.IsSuccessStatusCode && teamResponse.IsSuccessStatusCode)
            {
                // Takımları Çözümle ve ViewBag'e At
                var teamJson = await teamResponse.Content.ReadAsStringAsync();
                var teams = JsonConvert.DeserializeObject<List<dynamic>>(teamJson);
                ViewBag.Teams = teams;

                // Oyuncu Bilgilerini Çözümle ve Modele Gönder
                var playerJson = await playerResponse.Content.ReadAsStringAsync();
                var playerData = JsonConvert.DeserializeObject<UpdatePlayerDto>(playerJson);

                return View(playerData);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updatePlayerDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // ÇÖZÜM: API adresi sonuna oyuncunun ID'sini ekliyoruz (api/Players/5 gibi)
            var responseMessage = await client.PutAsync($"https://localhost:7026/api/Players/{updatePlayerDto.PlayerId}", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Eğer hata alırsak (API başarısız dönerse) sayfada kalıp takımları yeniden yükleyelim ki form bozulmasın
            var teamResponse = await client.GetAsync("https://localhost:7026/api/Teams");
            if (teamResponse.IsSuccessStatusCode)
            {
                var teamJson = await teamResponse.Content.ReadAsStringAsync();
                ViewBag.Teams = JsonConvert.DeserializeObject<List<dynamic>>(teamJson);
            }

            return View(updatePlayerDto);
        }
    }
}
