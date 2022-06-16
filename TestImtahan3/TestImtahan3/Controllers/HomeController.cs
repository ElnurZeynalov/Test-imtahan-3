using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestImtahan3.DAL;
using TestImtahan3.Models;

namespace TestImtahan3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<MenuItem> menuItems = _context.MenuItems.ToList();
            return View(menuItems);
        }
    }
}
