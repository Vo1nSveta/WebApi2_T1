using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WorkshopClientApp.Data;
using WorkshopClientApp.Models;
using WorkshopClientApp.Services;

namespace WorkshopClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly WorkshopClientService _workshopClientService;
        public HomeController(ILogger<HomeController> logger, WorkshopClientService workshopClientService)
        {
            _logger = logger;
            _workshopClientService = workshopClientService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nameWorkshop, string groupWorkshop)
        {
            var responseData = await _workshopClientService.SearchWorkshops(nameWorkshop, groupWorkshop);
         
            return View(responseData);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var worksopData = await _workshopClientService.GetWorkshopAll();

            return View(worksopData);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkshopAddDto request)
        {
            await _workshopClientService.AddWorkshop(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var equipments = await _workshopClientService.GetEquipmentAll();

            return View(equipments);
        }

        #region -- Редактирование

        [HttpPost]
        public async Task<IActionResult> Edit(WorkshopEditDto request)
        {
            await _workshopClientService.Edit(request);
           
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseData = await _workshopClientService.GetWorkshopById(id);
              
            return View(responseData);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {

            await _workshopClientService.Remove(id);

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
