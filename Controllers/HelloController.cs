using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGRC.Models;

namespace TGRC.Controllers;

public class HelloController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TGRCContext _context;

    public HelloController(ILogger<HomeController> logger, TGRCContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Accessions()
    {
        var model = await _context.Accessions.Where(a => a.AccessionNum == "LA0192").FirstOrDefaultAsync();
        return View(model);
    }

    
}
