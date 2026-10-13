using Microsoft.AspNetCore.Mvc;
using WorkshopWebApi.Data;
using WorkshopWebApi.Models;

namespace WorkshopWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        
        private readonly ILogger<EquipmentController> _logger;

        private readonly MyAppContext _context;

        public EquipmentController(ILogger<EquipmentController> logger, MyAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet()]
       
        public List<Equipment> Get()
        {
            _logger.LogInformation("Получаю список оборудования");

            return _context.Equipment.ToList();
        }

        [HttpGet("{id}")]

        public Equipment? Get(int id)
        {
            return _context.Equipment
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost()]
        public IEnumerable<Equipment> Post(string? nameEquipment, string? numberEquipment)
        {
            _logger.LogInformation("Получаю список оборудования с фильтром");

            var query = _context.Equipment.AsQueryable();

            if (!string.IsNullOrEmpty(nameEquipment))
            {
                query = query.Where(x => x.NameEquipment.Contains(nameEquipment));

            }

            if (!string.IsNullOrEmpty(numberEquipment))
            {
                query = query.Where(x => x.NumberEquipment.Contains(numberEquipment));

            }

            var equipment = query.ToList();


            return equipment;
        }

        [HttpPut()]
        public Equipment Put(EquipmentAdd request)
        {
            _logger.LogInformation("Добавляю новое оборудование");

            var equipment = new Equipment
            {
                NameEquipment = request.NameEquipment,
                NumberEquipment = request.NumberEquipment
            };

            _context.Equipment.Add(equipment);
            _context.SaveChanges();

            return equipment;
        }

        [HttpDelete()]
        public IActionResult Delete( int equipmentId)
        { 
            var equipment = _context.Equipment.FirstOrDefault(x => x.Id == equipmentId);

            if (equipment != null)
            {
                _context.Remove(equipment);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
            
        }

        [HttpPatch()]
        public IActionResult Patch(Equipment model)
        {
            var equipment = _context.Equipment.FirstOrDefault(x => x.Id == model.Id);

            if (equipment != null)
            {
                equipment.NameEquipment = model.NameEquipment;
                equipment.NumberEquipment = model.NumberEquipment;

                _context.Update(equipment);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();

        }
    }

    
    
}
