using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGRC.Models;

namespace TGRC.Controllers;

public class ImagesController : Controller
{
    private readonly TGRCContext _context;

    public ImagesController(TGRCContext context)
    {       
        _context = context;
    }

    public async Task<IActionResult> Detail(string id, string frame = "no")
    {
        var model = await ImageDetailsViewModel.Create(_context, id);
        ViewBag.Frame = frame;
        return View(model);
    }

    public async Task<IActionResult> Search(ImageSearchViewModel vm)
    {
        if(!vm.Search)
        {
            var freshModel = await ImageSearchViewModel.Create(_context, null);
            return View(freshModel);
        }
        var model = await ImageSearchViewModel.Create(_context, vm);
        return View(model);
    }

    
}
