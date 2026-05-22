using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using matchsite.WebUI.Dtos.MatchDto;
using matchsite.WebUI.Dtos.StandingDto;

namespace matchsite.WebUI.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LayoutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Matches");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var allMatches = JsonConvert.DeserializeObject<List<ResultMatchDto>>(jsonData);

                if (allMatches != null)
                {
                    // 1. ÖNE ÇIKAN MAÇLAR: Sadece Arsenal veya Liverpool'un yaklaşan maçları
                    var featuredMatches = allMatches
                        .Where(m => m.MatchDate >= DateTime.Now && m.MatchStatus == "NS" &&
                                   (m.HomeTeamName == "Arsenal" || m.AwayTeamName == "Arsenal" ||
                                    m.HomeTeamName == "Liverpool" || m.AwayTeamName == "Liverpool"))
                        .OrderBy(m => m.MatchDate)
                        .Take(2)
                        .ToList();

                    ViewBag.FeaturedMatches = featuredMatches;

                    // 2. TAMAMLANAN MAÇLAR (Son 4 Maç)
                    var finishedMatches = allMatches
                        .Where(m => m.MatchStatus == "FT" || m.IsFinished)
                        .OrderByDescending(m => m.MatchDate)
                        .Take(4)
                        .ToList();

                    ViewBag.FinishedMatches = finishedMatches;


                    var upcomingMatches = allMatches
                        .Where(m => m.MatchDate >= DateTime.Now && m.MatchStatus == "NS")
                        .OrderBy(m => m.MatchDate)
                        .Take(4)
                        .ToList();

                    ViewBag.UpcomingMatches = upcomingMatches;
                }
            }
            else
            {
                ViewBag.FeaturedMatches = new List<ResultMatchDto>();
                ViewBag.FinishedMatches = new List<ResultMatchDto>();
                ViewBag.UpcomingMatches = new List<ResultMatchDto>(); 
            }

            return View();
        }
        public async Task<IActionResult> StandingList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Standings"); 

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var standings = JsonConvert.DeserializeObject<List<ResultStandingDto>>(jsonData);

                return View(standings);
            }
            return View(new List<ResultStandingDto>());
        }
        public async Task<IActionResult> LastMatchList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7026/api/Matches");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var allMatches = JsonConvert.DeserializeObject<List<ResultMatchDto>>(jsonData);

                if (allMatches != null)
                {
                    var lastTenMatches = allMatches
                        .Where(m => m.MatchStatus == "FT" || m.IsFinished)
                        .OrderByDescending(m => m.MatchDate)
                        .Take(10)
                        .ToList();
                    return View(lastTenMatches);
                }
            }
            return View(new List<ResultMatchDto>());
        }
        public async Task<IActionResult> MatchDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7026/api/Matches/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var matchDetail = JsonConvert.DeserializeObject<ResultMatchDto>(jsonData);
                return View(matchDetail);
            }
            return RedirectToAction("Index");
        }
    }
}