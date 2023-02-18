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

        public List<GenesAndAllelesInImage> genes { get; set; }
        public string taxon { get; set; }       
        
               
        public static async Task<ImageDetailsViewModel> Create(TGRCContext _context, string id)
        { 
            var imageEntry = await _context.Images.Where(i => i.File == id).FirstOrDefaultAsync();
            var acc = await _context.AccessionsInImages.Where(i => i.ImageNum == imageEntry.ImageNum).ToListAsync();
            var model = new ImageDetailsViewModel
            {
                image = imageEntry,
                accession = acc,
                genes = await _context.GenesAndAllelesInImages.Where(i => i.ImageNum == imageEntry.ImageNum).ToListAsync(),
                taxon = await _context.Accessions.Where(a => a.AccessionNum == acc.First().AccessionNum).Select(a => a.Taxon2).FirstOrDefaultAsync()
            };           
            
            return model;
        }

       
    }    
}