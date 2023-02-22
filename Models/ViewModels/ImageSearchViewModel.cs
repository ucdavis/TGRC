using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TGRC.Models
{   
    
    public class ImageSearchViewModel
    {
        public List<GenesAndAllelesInImage> image { get; set; }

        public bool Search { get; set; }

        public string SelectedGene { get; set; }
        public List<string> GeneList { get; set; }
        public string SelectedPhenotypeCategory { get; set; }
        public List<PhenotypicCategory> PhenotypeCategoryList { get; set; }
        public string SelectedAccession { get; set; }
        public List<string> AccessionList { get; set; }
        
        public ImageSearchViewModel() {
            Search = false;
        }
        
        public static async Task<ImageSearchViewModel> Create(TGRCContext _context, ImageSearchViewModel vm)
        { 
           var geneList = await _context.GenesAndAllelesInAccessions.Select(a => a.Gene).Distinct().OrderBy(a=>a).ToListAsync();
           geneList.Insert(0,"");
           
                              
            if(vm != null)
            {
                var imageToFind = _context.GenesAndAllelesInImages.Include(g=> g.Image).AsQueryable();                
                   
                if(!string.IsNullOrWhiteSpace(vm.SelectedGene))
                {
                    imageToFind = imageToFind.Where(i => i.Gene == vm.SelectedGene);
                }
               

                
                
                var viewModel = new ImageSearchViewModel
                {
                    image = await imageToFind.ToListAsync(),                    
                    SelectedGene = vm.SelectedGene,
                    GeneList = geneList,
                                        
                };  
                return viewModel;


            }
            var freshModel = new ImageSearchViewModel
            {
                image = new List<GenesAndAllelesInImage>(),
                GeneList = geneList,
            };           

            return freshModel;
        }

        
    } 
    
}