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
        var model = await ImageDetailsViewModel.Create(_context, id);
        return View(model);
    }

    
}
