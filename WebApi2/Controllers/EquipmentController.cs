using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopWebApi.Data;

namespace WorkshopWebApi.Controllers

{ 
    [ApiController]
    [Route("[controller]")]

    public class EquipmentController : ControllerBase
    {
        private readonly MyAppContext _context;

        public EquipmentController(ILogger<WorkshopController> logger, MyAppContext context)
        {
            _context = context;
        }

    [HttpGet]

        public List<Workshop> Get()
        {
            
            return _context.Workshop
            .ToList();
        }
    }
}
