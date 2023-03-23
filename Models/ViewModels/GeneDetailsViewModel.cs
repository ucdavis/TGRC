using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace TGRC.Models
{   
     
    public class GeneDetailsViewModel
    {
        public Gene  gene { get; set; }

        public List<GenesAndAllele> alleles { get; set; }       
        public List<GenesAndAllelesInAccession> accessions { get; set; }
        public List<GenesAndAllelesInImage> images { get; set; }
        
               
        public static async Task<GeneDetailsViewModel> Create(TGRCContext _context, string id)
        {             
            var model = new GeneDetailsViewModel
            {
               gene = await _context.Genes.Where(g => g.Gene1==id).FirstOrDefaultAsync(),
               alleles = await _context.GenesAndAlleles.Where(g => g.Gene == id).Include(a => a.PhenoTypeDetails).ThenInclude(d => d.Category).ToListAsync(),
               accessions = await _context.GenesAndAllelesInAccessions.Where(g => g.Gene == id && g.Accession.Status == "Active").ToListAsync(),
               images = await _context.GenesAndAllelesInImages.Where(g => g.Gene == id && g.Image.Web != 0).Include(g => g.Image).ToListAsync(),
            };           
            
            return model;
        }       
    }    
}