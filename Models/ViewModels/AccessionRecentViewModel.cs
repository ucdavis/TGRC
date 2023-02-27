using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TGRC.Models
{   
    
    public class AccessionRecentViewModel
    {
        public List<Accession> accessions { get; set; }

        public bool Search { get; set; }
        
        public List<short?> AccessionYears { get; set; }
        public short SelectedYear { get; set; }

        
        

        public AccessionRecentViewModel() {
            Search = false;
        }
        
        public static async Task<AccessionRecentViewModel> Create(TGRCContext _context, AccessionRecentViewModel vm)
        {  
           var yearList = await _context.Accessions.Where(a => a.AccessionYear.HasValue).Select(a => a.AccessionYear).Distinct().OrderByDescending(a=>a).ToListAsync();
          
                              
            if(vm != null)
            {
                var accToFind = _context.Accessions.AsQueryable();
                
                if(vm.SelectedYear != 0)
                {
                    accToFind = accToFind.Where(a => a.AccessionYear == vm.SelectedYear);
                }     
                
                
                
                var viewModel = new AccessionRecentViewModel
                {
                    accessions = await accToFind.OrderBy(a => a.AccessionNum).ToListAsync(),
                    AccessionYears = yearList,
                    SelectedYear = vm.SelectedYear
                };  
                return viewModel;


            }
            var freshModel = new AccessionRecentViewModel
            {
                accessions = new List<Accession>(),  
                AccessionYears = yearList,
            };           

            return freshModel;
        }

        
    } 
    
}