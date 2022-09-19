using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MachineStatusSystem.Models
{
    public class MachineDetails
    {
        [Key]
        public int MId { get; set; }
        public String MName { get; set; }
        public string Status { get; set; }
        public int Temp { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public int Name { get; set; }

    }
}
