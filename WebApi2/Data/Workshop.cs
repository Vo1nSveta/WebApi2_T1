using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkshopWebApi.Data
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }
        
        public string NameWorkshop { get; set; } = string.Empty;

        public string GroupWorkshop { get; set; } = string.Empty;

        [ForeignKey(nameof(Equipment))]
        public int EquipmentId { get; set; }

        public Equipment Equipment { get; set; }
    }
}
