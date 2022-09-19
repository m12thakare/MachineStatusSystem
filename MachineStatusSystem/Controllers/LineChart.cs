using MachineStatusSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineStatusSystem.Controllers
{
    public class LineChart : Controller
    {
        private readonly UserContext _Db;
        public LineChart( UserContext userContext)
        {
            _Db = userContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetDataLineChart()
        {
             var userList = _Db.User.ToList();
            var linq = from x in userList select x;


            var Query = from p in userList.GroupBy(p => p.Designation)
                        select new
                        {
                            count1 = p.Count(),
                            p.First().Designation,
                        };

           
            return View();
        }
    }
}
