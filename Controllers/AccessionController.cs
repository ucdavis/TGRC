using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGRC.Models;

namespace TGRC.Controllers;

public class AccessionController : Controller
{
    private readonly TGRCContext _context;
    private readonly IConfiguration _config;

    public AccessionController(TGRCContext context, IConfiguration config)
    {       
        _context = context;
        _config = config;
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
        ViewBag.APIKey = _config["googleMapAPIKey"];
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

    public async Task<IActionResult> Cluster(AccessionClusterViewModel vm)
    {
        if(!vm.Search)
        {
            var freshModel = await AccessionClusterViewModel.Create(_context, null);
            return View(freshModel);
        }
        var model = await AccessionClusterViewModel.Create(_context, vm);
        return View(model);
    }

    public async Task<IActionResult> Recent(AccessionRecentViewModel vm)
    {
        if(!vm.Search)
        {
            var freshModel = await AccessionRecentViewModel.Create(_context, null);
            return View(freshModel);
        }
        var model = await AccessionRecentViewModel.Create(_context, vm);
        return View(model);
    }

    public async Task<IActionResult> Frequent(AccessionFrequentViewModel vm)
    {        
        var model = await AccessionFrequentViewModel.Create(_context, vm);
        return View(model);
    }

    public async Task<IActionResult> Maps (AccessionMapViewModel vm)
    {
        var model = await AccessionMapViewModel.Create(_context, vm);
        ViewBag.APIKey = _config["googleMapAPIKey"];
        return View(model);

    }

    public async Task<IActionResult> ProvinceListForCountry (string id)
    {
        var model = await _context.Accessions.Where(a => a.Country==id && a.ProvinceOrDepartment != null).Select(a=> a.ProvinceOrDepartment).Distinct().OrderBy(a=>a).ToListAsync();
        model.Insert(0,"[Any]");
        return Json(model);
    }    

    public async Task<IActionResult> Simple (AccessionSimpleViewModel vm)
    {
        if(!vm.Search)
        {
            var freshModel = await AccessionSimpleViewModel.Create(_context, null);
            return View(freshModel);
        }
        var model = await AccessionSimpleViewModel.Create(_context, vm);
        return View(model);
    }
    
}
