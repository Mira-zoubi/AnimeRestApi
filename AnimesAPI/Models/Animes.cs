using System;
using System.ComponentModel.DataAnnotations;

namespace AnimesAPI.Models
{
    public class Animes
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public String Name { set; get; }
        public String ?Genre { set; get; }
         public String Secret { set; get; }
    }
}

