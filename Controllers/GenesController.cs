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

    public async Task<IActionResult> Detail(string id)
    {
        var model = await _context.Genes.Include(g => g.Alleles).ThenInclude(a => a.PhenoTypeDetails).ThenInclude(d => d.Category).Include(g => g.Accessions.Where(a => a.Accession.Status == "Active")).Where(g => g.Gene1==id).FirstOrDefaultAsync();
        return View(model);
    }

    
}
