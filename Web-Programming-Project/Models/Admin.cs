using System.ComponentModel.DataAnnotations;

namespace Web_Programming_Project.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        public string AdminUsername { get; set; }

        public string AdminPassword { get; set; }
    }
}
