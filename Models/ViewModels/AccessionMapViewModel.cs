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
    
    public class AccessionMapViewModel
    {
        public List<Accession> accessions { get; set; }

        public bool Search { get; set; }
        public string StatusSelected { get; set; }
        
        public string CountrySelected { get; set; }
        public List<string> ProvinceList { get; set; }
        public string ProvinceSelected { get; set; }
        
        

        public AccessionMapViewModel() {
            Search = false;
            CountrySelected = "[Any]";
            ProvinceSelected = "[Any]";
        }
        
        public static async Task<AccessionMapViewModel> Create(TGRCContext _context, AccessionMapViewModel vm)
        {             
            var provinces = await _context.Accessions.Where(a=> a.ProvinceOrDepartment != null).Select(a => a.ProvinceOrDepartment).Distinct().OrderBy(a=>a).ToListAsync();
            provinces.Insert(0,"[Any]");

            vm.ProvinceList = provinces;   

            var accList = _context.Accessions.AsQueryable();

            if(!string.IsNullOrWhiteSpace(vm.CountrySelected))
            {
                accList = accList.Where(a => a.Country == vm.CountrySelected);
            } 

            vm.accessions = await accList.ToListAsync();                     
                
            return vm;
           
        }

        
    } 
    
}