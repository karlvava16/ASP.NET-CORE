﻿using System.ComponentModel.DataAnnotations;

namespace MoviesRazorPages.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }
       
        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Salt { get; set; }
    }
}
