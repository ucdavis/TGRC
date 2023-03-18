using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace TGRC.Models
{   
     
    public class ImageDetailsViewModel
    {
        public Image  image { get; set; }

        public List<AccessionsInImage> accession { get; set; }

        public List<GenesAndAllelesInAccession> genes { get; set; }
        public List<ColleaguesInImage> colleagues { get; set; }
        public string taxon { get; set; }       
        
               
        public static async Task<ImageDetailsViewModel> Create(TGRCContext _context, string id)
        { 
            var imageEntry = await _context.Images.Where(i => i.File == id).FirstOrDefaultAsync();
            var acc = await _context.AccessionsInImages.Where(i => i.ImageNum == imageEntry.ImageNum).OrderBy(a => a.AccessionNum).ToListAsync();
            var model = new ImageDetailsViewModel
            {
                image = imageEntry,
                accession = acc,
                genes = await _context.GenesAndAllelesInAccessions.Where(g => acc.Select(a => a.AccessionNum).Distinct().ToList().Contains(g.AccessionNum)).ToListAsync(),
                taxon = await _context.Accessions.Where(a => a.AccessionNum == acc.First().AccessionNum).Select(a => a.Taxon2).FirstOrDefaultAsync(),
                colleagues = await _context.ColleaguesInImages.Where(c => c.ImageNum == imageEntry.ImageNum).Include(c => c.Colleague).ToListAsync(),
            };           
            
            return model;
        }

       
    }    
}