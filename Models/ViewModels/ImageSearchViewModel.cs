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
           var phenoCat = await _context.PhenotypicCategories.Distinct().OrderBy(a =>a.PhenotypicCategory1).ToListAsync();
           phenoCat.Insert(0, new PhenotypicCategory { PhenotypicCategory1 = "" });
           
                              
            if(vm != null)
            {
                var imageToFind = _context.GenesAndAllelesInImages.Include(g=> g.Image).AsQueryable();                
                   
                if(!string.IsNullOrWhiteSpace(vm.SelectedGene))
                {
                    imageToFind = imageToFind.Where(i => i.Gene == vm.SelectedGene);
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedPhenotypeCategory))
                {
                    imageToFind = imageToFind.Where(i => i.GeneAndAlleleDetails.PhenoTypeDetails.PhenotypicalCategory == vm.SelectedPhenotypeCategory);
                }
               

                
                
                var viewModel = new ImageSearchViewModel
                {
                    image = await imageToFind.ToListAsync(),                    
                    SelectedGene = vm.SelectedGene,
                    SelectedPhenotypeCategory = vm.SelectedPhenotypeCategory,
                    GeneList = geneList,
                    PhenotypeCategoryList = phenoCat,
                                        
                };  
                return viewModel;


            }
            var freshModel = new ImageSearchViewModel
            {
                image = new List<GenesAndAllelesInImage>(),
                GeneList = geneList,
                PhenotypeCategoryList = phenoCat
            };           

            return freshModel;
        }

        
    } 
    
}