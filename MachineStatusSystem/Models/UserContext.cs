using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineStatusSystem.Dto;

namespace MachineStatusSystem.Models
{
    public class UserContext :DbContext
    {
       public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }
     
        public DbSet<User> User { get; set; }
        public DbSet<MachineDetails> MachineDetails { get; set; }
         public DbSet<LoginUser> LoginUser{ get; set; }
         public DbSet<MachineStatusSystem.Dto.UserRegisterDto> UserRegisterDto { get; set; }
    }
}
