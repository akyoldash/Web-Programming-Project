using System.ComponentModel.DataAnnotations;

namespace Web_Programming_Project.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Director { get; set; }

        public string Composer { get; set; }

        public string Language { get; set; }

        public string Rating { get; set; }

        public string Writer { get; set; }

        public string Star_Actor { get; set; }

        public string Genre { get; set; }

        public string Synposis { get; set; }

        public string Comment { get; set; }

        public ICollection<User> Users { get; set; }    

    }
}
