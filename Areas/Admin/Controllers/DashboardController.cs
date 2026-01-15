using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeCam.Contexts;
using SafeCam.ViewModels.MemberViewModels;

namespace SafeCam.Areas.Admin.Controllers;

[Area("Admin")] 
[Authorize(Roles ="Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
