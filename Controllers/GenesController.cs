using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGRC.Models;

namespace TGRC.Controllers;

public class GenesController : Controller
{
    private readonly TGRCContext _context;

    public GenesController(TGRCContext context)
    {       
        _context = context;
    }

    public async Task<IActionResult> Detail(string id, string frame = "no")
    {       
        var model = await GeneDetailsViewModel.Create(_context, id);
        ViewBag.Frame = frame;
        return View(model);
    }

    public async Task<IActionResult> Search(GeneSearchViewModel vm)
    {
        if(!vm.Search)
        {
            var freshModel = await GeneSearchViewModel.Create(_context, null);
            return View(freshModel);
        }
        var model = await GeneSearchViewModel.Create(_context, vm);
        return View(model);
    }  

    
}
