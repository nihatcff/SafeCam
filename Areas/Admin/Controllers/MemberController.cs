using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeCam.Contexts;
using SafeCam.Helpers;
using SafeCam.Models;
using SafeCam.ViewModels.MemberViewModels;

namespace SafeCam.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MemberController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public MemberController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");
    }

    public async Task<IActionResult> Index()
    {
        var members = await _context.Members.Select(x=>new MemberGetVM()
        {
            Id = x.Id,
            Fullname = x.Fullname,
            Designation = x.Designation,
            ImagePath = x.ImagePath
        }).ToListAsync();

        return View(members);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(MemberCreateVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("Image", "Size must be less than 2 mb");
            return View(vm);
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("Image", "You must upload Image");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.UploadFileAsync(_folderPath);

        Member member = new()
        { 
            Fullname = vm.Fullname,
            Designation = vm.Designation,
            ImagePath = uniqueFileName
        };

        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var member = await _context.Members.FindAsync(id);

        if (member is null)
        {
            return NotFound();
        }

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();

        string deletedImagePath = Path.Combine(_folderPath, member.ImagePath);

        ExtensionMethods.DeleteFile(deletedImagePath);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var member = await _context.Members.FindAsync(id);

        if (member is null)
            return NotFound();

        MemberUpdateVM vm = new()
        {
            Id = member.Id,
            Fullname = member.Fullname,
            Designation = member.Designation
        };

        return View(vm);

    }

    [HttpPost]
    public async Task<IActionResult> Update(MemberUpdateVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Size must be less than 2 mb");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "You must upload Image");
            return View(vm);
        }

        var existMember = await _context.Members.FindAsync(vm.Id);

        if (existMember is null)
            return BadRequest();

        existMember.Fullname = vm.Fullname;
        existMember.Designation = vm.Designation;

        if (vm.Image is { })
        {
            string newImagePath = await vm.Image.UploadFileAsync(_folderPath);
            string oldImagePath = Path.Combine(_folderPath, existMember.ImagePath);
            ExtensionMethods.DeleteFile(oldImagePath);
            existMember.ImagePath = newImagePath;
        }

        _context.Members.Update(existMember);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
