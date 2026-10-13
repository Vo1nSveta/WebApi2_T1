using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WorkshopClientApp.Data;
using WorkshopClientApp.Models;

namespace WorkshopClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nameWorkshop, string groupWorkshop)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5514/Workshop");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Workshop>());
            }

            var responseText = await response.Content.ReadAsByteArrayAsync();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var workshops = JsonSerializer.Deserialize<List<Workshop>>(responseText, jsonOptions) ?? new List<Workshop>();

            var query = workshops.AsQueryable();

            if (!string.IsNullOrEmpty(nameWorkshop))
            {
                query = query.Where(x => x.NameWorkshop != null && x.NameWorkshop.Contains(nameWorkshop));
            }

            if (!string.IsNullOrEmpty(groupWorkshop))
            {
                query = query.Where(x => x.GroupWorkshop != null && x.GroupWorkshop.Contains(groupWorkshop));
            }

            var Workshop = query.ToList();

            ViewBag.NameWorkshop = nameWorkshop;
            ViewBag.GroupWorkshop = groupWorkshop;

            return View(Workshop);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5514/Workshop");
            var responseText = await response.Content.ReadAsByteArrayAsync();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var responseData = JsonSerializer.Deserialize<List<Workshop>>(responseText, jsonOptions);

            return View(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkshopAddDto request)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync("http://localhost:5514/Workshop", request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        #region -- Редактирование

        [HttpPost]
        public async Task<IActionResult> Edit(WorkshopEditDto request)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PutAsJsonAsync("http://localhost:5514/Workshop", request);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5514/Workshop/{id}");
            var responseText = await response.Content.ReadAsByteArrayAsync();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var responseData = JsonSerializer.Deserialize<Workshop>(responseText, jsonOptions);

            return View(responseData);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {

            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"http://localhost:5514/Workshop?workshopId={id}");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
