using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TGRC.Models
{   
    
    public class AccessionDetailsViewModel
    {
        public Accession accession { get; set; }

        public int MarkerCount { get; set; }
        public string Markers { get; set; }


        public static async Task<AccessionDetailsViewModel> Create(TGRCContext _context, string id)
        {
            var acc = await _context.Accessions
                .Include(a => a.Donors).ThenInclude(d => d.Colleague)
                .Include(a => a.Categories)
                .Include(a => a.Cultures).ThenInclude(c => c.Recommendation)
                .Include(a => a.Genes)
                .Include(a => a.Images.Where(a => a.Image.Web != 0)).ThenInclude(i => i.Image)
                .Where(a => a.AccessionNum == id).FirstOrDefaultAsync();
            var related = await _context.Accessions.Where(a => a.LatDec.HasValue && a.LonDec.HasValue && ((a.Taxon2 == acc.Taxon2 && a.Status == "Active") || (a.AccessionNum == id))).ToListAsync();
            var groupList = related.GroupBy(a => new {a.LatDec, a.LonDec}).Select(g => new AccessionPin { 
                        LatDec = g.Key.LatDec.Value,
                        LonDec = g.Key.LonDec.Value,
                        AccessionNum = string.Join(", ", g.Select(a => a.AccessionNum)), 
                        Taxon2 = string.Join(", ", g.Select(a => a.Taxon2)),
                        Reference = string.Join(", ", g.Select(a => a.Reference.Replace("'","&#39").Replace(",","&#44"))),
                        Title = string.Join(", ", g.Select(a => $"{a.AccessionNum}({a.Taxon2})")),
                        Icon = g.Any(a => a.AccessionNum == id) ? "primary" : "related" 
                    }).ToList();
             StringBuilder marker = new StringBuilder();  
                groupList.ForEach(a =>  marker.Append($"['{a.LinkText}', {a.LatDec}, {a.LonDec}, '{a.Icon}','{a.Reference}'],"));  
                if(marker.Length > 2)
                {
                    marker.Remove(marker.Length - 1,1);
                }              
               
            var viewModel = new AccessionDetailsViewModel
            {
                accession = acc,
                Markers = marker.ToString(),
                MarkerCount = groupList.Count,
            };  
            return viewModel;

        }
    }
}
