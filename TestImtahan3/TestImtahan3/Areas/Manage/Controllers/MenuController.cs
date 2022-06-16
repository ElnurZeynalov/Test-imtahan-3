using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestImtahan3.DAL;
using TestImtahan3.Models;

namespace TestImtahan3.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MenuController : Controller
    {
        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public MenuController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<MenuItem> menuList = await _context.MenuItems.ToListAsync();
            return View(menuList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (!ModelState.IsValid) return View();
            if (menuItem == null) return View();
            string fileName = Guid.NewGuid().ToString() + menuItem.Image.FileName;
            using (FileStream fs = new FileStream(Path.Combine(_env.WebRootPath, "assets", "image", fileName), FileMode.Create))
            {
                await menuItem.Image.CopyToAsync(fs);
            }
            menuItem.ImageUrl = fileName;
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            MenuItem menuItem = _context.MenuItems.Find(id);
            return View(menuItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuItem newMenuItem, int id)
        {
            if (newMenuItem == null) return View();
            if (newMenuItem.Id == id) return BadRequest();
            MenuItem oldMenuItem = _context.MenuItems.Find(id);
            if (newMenuItem.Image != null)
            {
                if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, "assets", "image", oldMenuItem.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(_env.WebRootPath, "assets", "image", oldMenuItem.ImageUrl));
                }
                string fileName = Guid.NewGuid().ToString() + newMenuItem.Image.FileName;
                using (FileStream fs = new FileStream(Path.Combine(_env.WebRootPath, "assets", "image", fileName), FileMode.Create))
                {
                    await newMenuItem.Image.CopyToAsync(fs);
                }
                newMenuItem.ImageUrl = fileName;
            }
            oldMenuItem.Name = newMenuItem.Name;
            oldMenuItem.Description = newMenuItem.Description;
            oldMenuItem.Price = newMenuItem.Price;
            oldMenuItem.CategoryId = newMenuItem.CategoryId;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            MenuItem menuItem = _context.MenuItems.Find(id);
            if(menuItem != null) return View();
            if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, "assets", "image", menuItem.ImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(_env.WebRootPath, "assets", "image", menuItem.ImageUrl));
            }
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
