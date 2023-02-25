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
        var model = await _context.Genes
            .Include(g => g.Alleles).ThenInclude(a => a.PhenoTypeDetails).ThenInclude(d => d.Category)
            .Include(g => g.GeneAccessions.Where(a => a.Accession.Status == "Active"))
            .Include(g => g.GeneImages).ThenInclude(i => i.Image)
            .Where(g => g.Gene1==id).FirstOrDefaultAsync();
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

    public async Task<IActionResult> GetAlleleForGene(string id)
    {
        var alleles = await _context.GenesAndAlleles.Where(g => g.Gene == id).Select(a => a.Allele).Distinct().OrderBy(a=>a).ToListAsync();
        alleles.Insert(0, "");
        return Json(alleles);
    }

    
}
