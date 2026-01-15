using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeCam.Contexts;
using SafeCam.ViewModels.MemberViewModels;

namespace SafeCam.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.Select(x => new MemberGetVM()
            {
                Fullname = x.Fullname,
                Designation = x.Designation,
                ImagePath = x.ImagePath
            }).ToListAsync();

            return View(members);
        }
    }
}
