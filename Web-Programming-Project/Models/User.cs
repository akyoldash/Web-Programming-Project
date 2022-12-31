﻿using System.ComponentModel.DataAnnotations;

namespace Web_Programming_Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
