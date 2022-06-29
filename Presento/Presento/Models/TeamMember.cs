using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Presento.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(25)]
        public string Profession { get; set; }
        [Required]
        [MaxLength(100)]
        public string Twitter { get; set; }
        [Required]
        [MaxLength(100)]
        public string Facebook { get; set; }
        [Required]
        [MaxLength(100)]
        public string Instagram { get; set; }
        [Required]
        [MaxLength(100)]
        public string Linkedin { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
