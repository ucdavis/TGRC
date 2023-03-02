using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace TGRC.Models
{   
    
    public class AccessionFrequentViewModel
    {
        public List<RequestSummary> accessions { get; set; }

        public bool Search { get; set; }
        
        public List<int> ShipYears { get; set; }
        public int SelectedYear { get; set; }

        
        

        public AccessionFrequentViewModel() {
            Search = false;
        }
        
        public static async Task<AccessionFrequentViewModel> Create(TGRCContext _context, AccessionFrequentViewModel vm)
        {  
           var yearList = await _context.Requests.Where(r => r.ShippingDate != null).Select(r => r.ShippingDate.Value.Year).Distinct().OrderByDescending(a=>a).ToListAsync();
          
            if(vm.SelectedYear == 0)
            {
                var freshModel = new AccessionFrequentViewModel
                {                  
                    SelectedYear = yearList.First()
                };   
                vm = freshModel; 

            }   

            var p0 = new SqlParameter("@year", vm.SelectedYear);
            vm.accessions = await _context.RequstSummaries.FromSqlRaw($"EXEC requestSummary @year", p0).ToListAsync();                
            vm.ShipYears = yearList;               
                
            return vm;
           
        }

        
    } 
    
}