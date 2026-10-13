using System.ComponentModel.DataAnnotations;

namespace WorkshopWebApi.Data
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        
        public string NameEquipment { get; set; } = string.Empty;

        public string NumberEquipment { get; set; } = string.Empty;
    }
}
