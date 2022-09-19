using MachineStatusSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineStatusSystem.Repository
{
    public interface IAuthRepository
    {
        int Register(LoginUser user, string password);

        string Login(string userName, string password);

        bool UserExists(string userName);
    }
}
