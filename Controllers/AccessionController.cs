using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGRC.Models;

namespace TGRC.Controllers;

public class AccessionController : Controller
{
    private readonly TGRCContext _context;

    public AccessionController(TGRCContext context)
    {       
        _context = context;
    }

    public async Task<IActionResult> Detail(string id, string frame = "no")
    {
        var model = await _context.Accessions
            .Include(a => a.Donors).ThenInclude(d => d.Colleague)
            .Include(a => a.Categories)
            .Include(a => a.Cultures).ThenInclude(c => c.Recommendation)
            .Include(a => a.Genes)
            .Include(a => a.Images).ThenInclude(i => i.Image)
            .Where(a => a.AccessionNum == id).FirstOrDefaultAsync();
        ViewBag.Frame = frame;
        return View(model);
    }

    public async Task<IActionResult> Search(AccessionSearchViewModel vm)
    {
        if(!vm.Search)
        {
            var freshModel = await AccessionSearchViewModel.Create(_context, null);
            return View(freshModel);
        }
        var model = await AccessionSearchViewModel.Create(_context, vm);
        return View(model);
    }

    
}
