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
        public List<Image> image { get; set; }

        public bool Search { get; set; }

        public string SelectedGene { get; set; }
        public List<string> GeneList { get; set; }
        public string SelectedPhenotypeCategory { get; set; }
        public List<PhenotypicCategory> PhenotypeCategoryList { get; set; }
        public string SelectedAccession { get; set; }
        public List<string> AccessionList { get; set; }
        public string SelectedTaxon { get; set; }
        public List<Taxa> Taxons { get; set; }
        public string AccessionCategoryToSearch { get; set; }
        public List<string> AccessionCategories { get; set; }
        public int SelectedContributor { get; set; }
        public List<Colleague> ContributorList { get; set; }
        public string CaptionSearchString { get; set; }
        
        public ImageSearchViewModel() {
            Search = false;
        }
        
        public static async Task<ImageSearchViewModel> Create(TGRCContext _context, ImageSearchViewModel vm)
        { 
           var geneList = await _context.GenesAndAllelesInAccessions.Select(a => a.Gene).Distinct().OrderBy(a=>a).ToListAsync();
           geneList.Insert(0,"");
           var phenoCat = await _context.PhenotypicCategories.Distinct().OrderBy(a =>a.PhenotypicCategory1).ToListAsync();
           phenoCat.Insert(0, new PhenotypicCategory { PhenotypicCategory1 = "" });
           var accList = await _context.Accessions.Where(a => a.Images.Any()).Select(a=>a.AccessionNum).OrderBy(a=>a).ToListAsync();
           accList.Insert(0,"");
           var taxa = await _context.Taxa.OrderByDescending(t => t.L).ThenBy(t => t.Taxon).ToListAsync();
           taxa.Insert(0, new Taxa { Taxon = "", CompleteName = "", Synonym = "", L="z"});
           var cat = await _context.AccessionCategories.Select(c => c.AccessionCategory).Distinct().ToListAsync();
           cat.Insert(0, "");
           var contrib = await _context.Colleagues.Where(c => c.Images.Any()).Distinct().OrderBy(c => c.LastName).ThenBy(c=> c.FirstName).ToListAsync();
           contrib.Insert(0, new Colleague{ ColleagueNum=0, FirstName = "", MiddleInitial = "", LastName=""});
                              
            if(vm != null)
            {
                
                bool accSearched = false;
                bool imageSearch = false;
                bool genesSearched = false;
                var accNumList = _context.Accessions.AsQueryable();  
                var imageSearchList = _context.Images.AsQueryable(); 
                var geneAlleleImages = _context.GenesAndAllelesInImages.Include(g => g.Image).AsQueryable();   
                var foundImage = new List<Image>(); 
                var accImages = new List<Image>(); 
                var geneImage = new List<Image>();   
                var imageImages = new List<Image>();    
                   
                if(!string.IsNullOrWhiteSpace(vm.SelectedGene))
                {
                    geneAlleleImages = geneAlleleImages.Where(i => i.Gene == vm.SelectedGene);
                    genesSearched = true;
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedPhenotypeCategory))
                {
                    geneAlleleImages = geneAlleleImages.Where(i => i.GeneAndAlleleDetails.PhenoTypeDetails.PhenotypicalCategory == vm.SelectedPhenotypeCategory);
                    genesSearched = true;
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedAccession))
                {
                    accNumList = accNumList.Where(a => a.AccessionNum == vm.SelectedAccession);
                    accSearched = true;
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedTaxon))
                {
                    accNumList = accNumList.Where(a => a.Taxon2 == vm.SelectedTaxon || a.Taxon == vm.SelectedTaxon);
                    accSearched = true;
                } 
                if(!string.IsNullOrWhiteSpace(vm.AccessionCategoryToSearch))
                {
                    accNumList = accNumList.Where(a => a.Categories.Any(c=> c.AccessionCategory == vm.AccessionCategoryToSearch));
                    accSearched = true;
                }

                if(vm.SelectedContributor != 0)
                {
                    imageSearchList = imageSearchList.Where(i => i.Contributors.Any(c => c.ColleagueNum == vm.SelectedContributor));                       
                    imageSearch = true;                 
                }

                if(!string.IsNullOrWhiteSpace(vm.CaptionSearchString))
                {
                    imageSearchList = imageSearchList.Where(i => EF.Functions.Like(i.Caption, "%" + vm.CaptionSearchString + "%" ));
                    imageSearch = true;
                }

                if(genesSearched)
                {
                    geneImage = await geneAlleleImages.Select(g => g.Image).ToListAsync();
                }                
                if(accSearched)
                {
                    var accNumbers = await accNumList.Select(a => a.AccessionNum).ToListAsync();
                    accImages = await _context.AccessionsInImages.Where(i => accNumbers.Contains(i.AccessionNum)).Select(i => i.Image).ToListAsync();
                }
                if(imageSearch)
                {
                    imageImages = await imageSearchList.ToListAsync();
                }
                

                 if(genesSearched && accSearched && imageSearch)
                {
                    foundImage = geneImage.Intersect(accImages).Intersect(imageImages).ToList();
                } else if(genesSearched && accSearched)
                {
                    foundImage = geneImage.Intersect(accImages).ToList();
                } else if(accSearched && imageSearch)
                {
                    foundImage = accImages.Intersect(imageImages).ToList();
                } else if(genesSearched)
                {
                    foundImage = geneImage;
                } else if(accSearched)
                {
                    foundImage = accImages;
                } else if(imageSearch)
                {
                    foundImage = imageImages;
                } else {
                    foundImage = await imageSearchList.ToListAsync();
                }

               

                
                var viewModel = new ImageSearchViewModel
                {
                    image = foundImage.Where(i => i.Web != 0 ).Distinct().ToList(),                    
                    SelectedGene = vm.SelectedGene,
                    SelectedPhenotypeCategory = vm.SelectedPhenotypeCategory,
                    GeneList = geneList,
                    PhenotypeCategoryList = phenoCat,
                    AccessionList = accList,     
                    SelectedAccession = vm.SelectedAccession,  
                    Taxons = taxa,
                    SelectedTaxon = vm.SelectedTaxon,
                    AccessionCategories = cat,
                    AccessionCategoryToSearch = vm.AccessionCategoryToSearch, 
                    ContributorList = contrib,
                    SelectedContributor = vm.SelectedContributor,
                    CaptionSearchString = vm.CaptionSearchString,
                    Search = true
                };  
                return viewModel;


            }
            var freshModel = new ImageSearchViewModel
            {
                image = new List<Image>(),
                GeneList = geneList,
                PhenotypeCategoryList = phenoCat,
                AccessionList = accList,
                Taxons = taxa,
                AccessionCategories = cat,
                ContributorList = contrib,
                Search = false
            };           

            return freshModel;
        }

        
    } 
    
}