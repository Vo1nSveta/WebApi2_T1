using System.ComponentModel.DataAnnotations;

namespace WorkshopWebApi.Data
{
    public class Workshop
    {
        [Key]
        public int Id { get; set; }
        
        public string NameWorkshop { get; set; } = string.Empty;

        public string GroupWorkshop { get; set; } = string.Empty;
    }
}
