using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachineStatusSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "required")]
        public int Mobile { get; set; }
        [Required(ErrorMessage = "required")]
        public string Designation { get; set; }

      
    }
}
