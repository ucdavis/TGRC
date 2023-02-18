using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TGRC.Models
{   
    public enum SearchOptions
    { 
        No,
        Yes,
        Both        
    }    
    public class AccessionSearchViewModel
    {
        public List<Accession> accessions { get; set; }

        public bool Search { get; set; }
        
        public string AccessionNumberToSearch { get; set; }

        public string StatusToSearch { get; set; }
        public string InspectedSearch { get; set; }
        public string OtherIdSearch { get; set; }
        public string SelectedTaxon { get; set; }
        public List<Taxa> Taxons { get; set; }
        

        public AccessionSearchViewModel() {
            Search = false;
        }
        
        public static async Task<AccessionSearchViewModel> Create(TGRCContext _context, AccessionSearchViewModel vm)
        {   
           var taxa = await _context.Taxa.OrderByDescending(t => t.L).ThenBy(t => t.Taxon).ToListAsync();
           taxa.Insert(0, new Taxa { Taxon = "", CompleteName = "", Synonym = "", L="z"});
                              
            if(vm != null)
            {
                var accToFind = _context.Accessions
                    .Include(a => a.Donors).ThenInclude(d => d.Colleague)
                    .Include(a => a.Categories)
                    .Include(a => a.Cultures).ThenInclude(c => c.Recommendation)
                    .Include(a => a.Genes)
                    .Include(a => a.Images).ThenInclude(i => i.Image)
                    .AsQueryable();
                
                if(!string.IsNullOrWhiteSpace(vm.AccessionNumberToSearch))
                {
                    accToFind = accToFind.Where(a => EF.Functions.Like(a.AccessionNum, "%" + vm.AccessionNumberToSearch + "%"));
                }     
                if(!string.IsNullOrWhiteSpace(vm.StatusToSearch))
                {
                    accToFind = accToFind.Where(a => a.Status == vm.StatusToSearch);
                }
                if(!string.IsNullOrWhiteSpace(vm.InspectedSearch))
                {
                    if(vm.InspectedSearch == "Yes")
                    {
                        accToFind = accToFind.Where(a => a.Inspected);
                    } else {
                        accToFind = accToFind.Where(a => !a.Inspected);
                    }
                    
                }
                if(!string.IsNullOrWhiteSpace(vm.OtherIdSearch))
                {
                    accToFind = accToFind.Where(a => EF.Functions.Like(a.OtherId, "%" + vm.OtherIdSearch + "%"));
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedTaxon))
                {
                    accToFind = accToFind.Where(a => a.Taxon2 == vm.SelectedTaxon);
                }
                
                var viewModel = new AccessionSearchViewModel
                {
                    accessions = await accToFind.ToListAsync(),
                    Taxons = taxa,
                };  
                return viewModel;


            }
            var freshModel = new AccessionSearchViewModel
            {
                accessions = new List<Accession>(),  
                Taxons = taxa,              
            };           

            return freshModel;
        }

        
    } 
    
}