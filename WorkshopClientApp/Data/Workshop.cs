using System.ComponentModel.DataAnnotations;

namespace WorkshopClientApp.Data
{
    public class Workshop
    {

        [Key]
        public int Id { get; set; }
        
        public string NameWorkshop { get; set; }
        public string GroupWorkshop { get; set; }
    }
}
