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

    public async Task<IActionResult> Detail(string id)
    {
        var model = await _context.Images.Where(i => i.File == id).FirstOrDefaultAsync();
        return View(model);
    }

    
}
