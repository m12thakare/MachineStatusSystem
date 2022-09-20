using MachineStatusSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineStatusSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _Db;

        public UserController(UserContext Db)
        {
            _Db = Db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(String UserSearch)//search
        {
          
            var userquery = from x in _Db.User select x;
           
            if(!string.IsNullOrEmpty(UserSearch))// not empty 
            {
                userquery = userquery.Where(x => x.Name.Contains(UserSearch));
            }
            return View(await userquery.AsNoTracking().ToListAsync());
        }

        public IActionResult UserList()
        {
            try
            {
                 var userList = _Db.User.ToList();
             
                var cnt = _Db.User.Count();
                          

                var Query = from p in userList.GroupBy(p => p.Designation)
                            select new
                            {
                                count1 = p.Count(),
                                p.First().Designation,
                            };

             
                ViewBag.totalcnt = cnt;
                 return View(userList);
            }
            catch(Exception e)
            {
                return View();
            }
           
        }
        
        [HttpGet]
        public  IActionResult Create(User obj)
        {
            return View(obj);
        }
       
        [HttpPost]
        public async Task<IActionResult> AddUser(User obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(obj.Id==0)// if new
                    {
                        _Db.User.Add(obj);
                        await _Db.SaveChangesAsync();
                    }
                    else// id > 0
                    {
                        _Db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //update the data inside  obj
                        await _Db.SaveChangesAsync();
                    }
                   
                    return RedirectToAction("UserList");
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("UserList");
            }
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var userget = await _Db.User.FindAsync(id);//get the record with id
                if(userget!=null)
                {
                    _Db.User.Remove(userget);// delete  and save changes
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("UserList");
            }
            catch (Exception e)
            {
               
                return RedirectToAction("UserList");
            }
        }

    }
}
