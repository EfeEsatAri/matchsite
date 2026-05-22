using matchsite.WebUI.Dtos.MatchDto;
using matchsite.WebUI.Dtos.TeamDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace matchsite.WebUI.Controllers
{
    public class AdminMatchesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMatchesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // --- MAÇLARI LİSTELEME ---
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10; // Bir sayfada gösterilecek maç sayısı
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Matches");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var allMatches = JsonConvert.DeserializeObject<List<ResultMatchDto>>(jsonData);

                // Toplam maç sayısını ve toplam sayfa sayısını hesaplayıp View'a gönderiyoruz
                int totalMatches = allMatches.Count;
                int totalPages = (int)Math.Ceiling((double)totalMatches / pageSize);

                // Sayfa sınırlarını kontrol etme (Güvenlik önlemi)
                if (page < 1) page = 1;
                if (page > totalPages && totalPages > 0) page = totalPages;

                // LINQ ile sadece o sayfaya ait maçları koparıp alıyoruz (Pagination Mantığı)
                var pagedMatches = allMatches
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Sayfalama bilgilerini ön yüze (View) taşımak için ViewBag kullanıyoruz
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.HasPreviousPage = page > 1;
                ViewBag.HasNextPage = page < totalPages;

                return View(pagedMatches);
            }
            return View(new List<ResultMatchDto>());
        }

        // --- MAÇ EKLEME (GET) ---
        [HttpGet]
        public async Task<IActionResult> CreateMatch()
        {
            // Maç eklerken ev sahibi ve deplasman takımlarını seçebilmek için 
            // Takımların listesini çekip ViewBag ile sayfaya göndermemiz gerekiyor.
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Teams");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var teams = JsonConvert.DeserializeObject<List<ResultTeamDto>>(jsonData);
                ViewBag.Teams = teams;
            }
            return View();
        }

        // --- MAÇ EKLEME (POST) ---
        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchDto createMatchDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMatchDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7026/api/Matches", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMatch(int id) // <-- Burası kesinlikle 'id' olmalı, 'MatchId' değil!
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Matches/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateMatchDto>(jsonData);
                return View(value);
            }

            return Content($"API'den veri çekilemedi! Durum Kodu: {responseMessage.StatusCode} - Gönderilen ID: {id}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMatch(UpdateMatchDto updateMatchDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateMatchDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync($"https://localhost:7026/api/Matches/{updateMatchDto.MatchId}", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateMatchDto);
        }

        // --- MAÇ SİLME ---
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7026/api/Matches/" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
