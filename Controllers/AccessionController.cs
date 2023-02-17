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

    public async Task<IActionResult> Detail(string id)
    {
        var model = await _context.Accessions.Include(a => a.Donors).ThenInclude(d => d.Colleague).Include(a => a.Categories).Include(a => a.Cultures).ThenInclude(c => c.Recommendation).Where(a => a.AccessionNum == id).FirstOrDefaultAsync();
        return View(model);
    }

    
}
