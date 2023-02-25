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
           var accList = await _context.Accessions.Select(a=>a.AccessionNum).OrderBy(a=>a).ToListAsync();
           accList.Insert(0,"");
           var taxa = await _context.Taxa.OrderByDescending(t => t.L).ThenBy(t => t.Taxon).ToListAsync();
           taxa.Insert(0, new Taxa { Taxon = "", CompleteName = "", Synonym = "", L="z"});
           var cat = await _context.AccessionCategories.Select(c => c.AccessionCategory).Distinct().ToListAsync();
           cat.Insert(0, "");
           var contrib = await _context.Colleagues.Where(c => c.Images.Any()).Distinct().OrderBy(c => c.LastName).ThenBy(c=> c.FirstName).ToListAsync();
           contrib.Insert(0, new Colleague{ ColleagueNum=0, FirstName = "", MiddleInitial = "", LastName=""});
                              
            if(vm != null)
            {
                var geneAlleleImages = _context.GenesAndAllelesInImages.Include(g=> g.Image).AsQueryable(); 
                var accNumList = _context.Accessions.AsQueryable();  
                var imageSearchList = _context.Images.AsQueryable();             
                   
                if(!string.IsNullOrWhiteSpace(vm.SelectedGene))
                {
                    geneAlleleImages = geneAlleleImages.Where(i => i.Gene == vm.SelectedGene);
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedPhenotypeCategory))
                {
                    geneAlleleImages = geneAlleleImages.Where(i => i.GeneAndAlleleDetails.PhenoTypeDetails.PhenotypicalCategory == vm.SelectedPhenotypeCategory);
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedAccession))
                {
                    accNumList = accNumList.Where(a => a.AccessionNum == vm.SelectedAccession);
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedTaxon))
                {
                    accNumList = accNumList.Where(a => a.Taxon2 == vm.SelectedTaxon || a.Taxon == vm.SelectedTaxon);
                } 
                if(!string.IsNullOrWhiteSpace(vm.AccessionCategoryToSearch))
                {
                    // Should this be from CategoriesInImages instead of AccessionCategoryInAccessions?????
                    accNumList = accNumList.Where(a => a.Categories.Any(c=> c.AccessionCategory == vm.AccessionCategoryToSearch));
                }
                               


                var geneImage = await geneAlleleImages.Select(g => g.Image).ToListAsync();
                var accNumbers = await accNumList.Select(a => a.AccessionNum).ToListAsync();
                var accImages = await _context.AccessionsInImages.Where(i => accNumbers.Contains(i.AccessionNum)).Select(i => i.Image).ToListAsync();

                var imageList = geneImage.Intersect(accImages).ToList();

                if(vm.SelectedContributor != 0)
                {
                    var contribImages = await _context.ColleaguesInImages.Where(c => c.ColleagueNum == vm.SelectedContributor).Select(c => c.Image).ToListAsync();
                    imageList = imageList.Intersect(contribImages).ToList();                    
                }

                if(!string.IsNullOrWhiteSpace(vm.CaptionSearchString))
                {
                    imageList = imageList.Where(i => i.Caption.Contains(vm.CaptionSearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }



                 
                
                var viewModel = new ImageSearchViewModel
                {
                    image = imageList.Where(i => i.Web != 0 ).ToList(),                    
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
            };           

            return freshModel;
        }

        
    } 
    
}