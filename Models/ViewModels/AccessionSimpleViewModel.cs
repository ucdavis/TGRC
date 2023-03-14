using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TGRC.Models;
using System.Text.RegularExpressions;

namespace TGRC.Models
{   
   
    public class AccessionSimpleViewModel
    {
        public List<Accession> accessions { get; set; }

        public bool Search { get; set; }
        
        public string SearchTerm { get; set; }        
        

        public AccessionSimpleViewModel() {
            Search = false;
        }
        
        public static async Task<AccessionSimpleViewModel> Create(TGRCContext _context, AccessionSimpleViewModel vm)
        {   
                                         
            if(vm != null)
            {
                var accToFind = _context.Accessions.AsQueryable();
                
                if(!string.IsNullOrWhiteSpace(vm.SearchTerm))
                {
                    var accPad = AccessionSearchViewModel.padAccessionSearch(vm.SearchTerm);
                    var accFuzzy = fuzzySearch(vm.SearchTerm);
                    accToFind = accToFind.Where(a => EF.Functions.Like(a.AccessionNum, "%" + vm.SearchTerm + "%") 
                        || EF.Functions.Like(a.AccessionNum, "%" + accPad + "%")
                        || EF.Functions.Like(a.AccessionNum, "%" + accFuzzy + "%")
                        || EF.Functions.Like(a.OtherId, "%" + vm.SearchTerm + "%") 
                        || EF.Functions.Like(a.OtherId, "%" + accPad + "%")
                        || EF.Functions.Like(a.OtherId, "%" + accFuzzy + "%")
                        || EF.Functions.Like(a.CultivarName, "%" + vm.SearchTerm + "%") 
                        || EF.Functions.Like(a.CultivarName, "%" + accPad + "%")
                        || EF.Functions.Like(a.CultivarName, "%" + accFuzzy + "%")
                        || EF.Functions.Like(a.Reference, "%" + vm.SearchTerm + "%") 
                        || EF.Functions.Like(a.Reference, "%" + accPad + "%")
                        || EF.Functions.Like(a.Reference, "%" + accFuzzy + "%"));
                }                    
                var viewModel = new AccessionSimpleViewModel
                {
                    accessions = await accToFind.OrderBy(a => a.AccessionNum).ToListAsync(),
                    SearchTerm = vm.SearchTerm,
                    Search = true,
                };  
                return viewModel;


            }
            var freshModel = new AccessionSimpleViewModel
            {
                accessions = new List<Accession>(),  
                SearchTerm = "",
                Search = false,
            };           

            return freshModel;
        }

        private static string fuzzySearch(string search)
        {
            if(search.IndexOf("-", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return search.Replace("-","%");
            }
            return Regex.Replace(search, "(?<=[A-Za-z])(?=[0-9])","%");
        }
        
    } 
    
}