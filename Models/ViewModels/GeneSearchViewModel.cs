using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TGRC.Models
{   
    
    public class GeneSearchViewModel
    {
        public List<Gene> genes { get; set; }

        public bool Search { get; set; }

        public string SelectedGene { get; set; }
        public List<string> GeneList { get; set; }
        public string SelectedLocus { get; set; }
        public List<string> LocusList { get; set; }
        public string SelectedAllele { get; set; }
        public List<string> AlleleList { get; set; }
        public string SynonymToSearch { get; set; }
        public string SelectedMarker { get; set; }
        public List<string> MarkerList { get; set; }  
        public string SelectedMutant { get; set; }
        public List<MutantType> MutantList { get; set; }
        public string SelectedChromosome { get; set; }
        public List<string> ChromosomeList { get; set; }
        public string SelectedPhenotypeCategory { get; set; }
        public List<PhenotypicCategory> PhenotypeCategoryList { get; set; }
        public string PhenotypeToSearch { get; set; }
        public string PhenotypeKeyword { get; set; }

        public GeneSearchViewModel() {
            Search = false;
        }
        
        public static async Task<GeneSearchViewModel> Create(TGRCContext _context, GeneSearchViewModel vm)
        { 
           var geneList = await _context.GenesAndAllelesInAccessions.Select(a => a.Gene).Distinct().OrderBy(a=>a).ToListAsync();
           geneList.Insert(0,"");
           var locusList = await _context.Genes.Where(g => g.LocusName != null).Select(a => a.LocusName).Distinct().OrderBy(a=>a).ToListAsync();
           locusList.Insert(0,"");
           var alleleList = await _context.GenesAndAlleles.Where(g => g.Allele != null).Select(a => a.Allele).Distinct().OrderBy(a=>a).ToListAsync();
           alleleList.Insert(0,"");
           var markerList = await _context.Genes.Where(g => g.MarkerType != null).Select(g => g.MarkerType).Distinct().OrderBy(a=>a).ToListAsync();
           markerList.Insert(0,"");
           var mutantList = await _context.GenesAndAlleles.Where(ga => ga.MutantType != null).Include(ga => ga.MutantTypeTranslation).Select(ga => ga.MutantTypeTranslation).Distinct().OrderBy(m=> m.MutMeaning).ToListAsync();
           mutantList.Insert(0, new MutantType { Type="0", MutMeaning=""});
           var chromosomeList = await _context.Genes.Where(g => g.Chromosome != null).Select(g => g.Chromosome).Distinct().OrderBy(a=>a).ToListAsync();
           chromosomeList.Insert(0, "");
           var phenoCat = await _context.PhenotypicCategories.Distinct().OrderBy(a =>a.PhenotypicCategory1).ToListAsync();
           phenoCat.Insert(0, new PhenotypicCategory { PhenotypicCategory1 = "" });

                              
            if(vm != null)
            {
                var geneToFind = _context.Genes.AsQueryable();                
                   
                if(!string.IsNullOrWhiteSpace(vm.SelectedGene))
                {
                    geneToFind = geneToFind.Where(g => g.Gene1 == vm.SelectedGene);
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedLocus))
                {
                    geneToFind = geneToFind.Where(g => g.LocusName == vm.SelectedLocus);
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedAllele))
                {
                    geneToFind = geneToFind.Where(g => g.Alleles.Any(a => a.Allele == vm.SelectedAllele));
                }
                if(!string.IsNullOrWhiteSpace(vm.SynonymToSearch))
                {
                    geneToFind = geneToFind.Where(g => g.Alleles.Any(a => EF.Functions.Like(a.SynonymsOfAllele, "%" + vm.SynonymToSearch + "%")));
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedMarker))
                {
                    geneToFind = geneToFind.Where(g => g.MarkerType == vm.SelectedMarker);
                }
                if(vm.SelectedMutant != "0")
                {
                    geneToFind = geneToFind.Where(g => g.Alleles.Any(a => a.MutantType == vm.SelectedMutant));
                }
                if(!string.IsNullOrWhiteSpace(vm.SelectedPhenotypeCategory))
                {
                    geneToFind = geneToFind.Where(g => g.Alleles.Any(a => a.PhenoTypeDetails.PhenotypicalCategory == vm.SelectedPhenotypeCategory));
                }
                if(!string.IsNullOrWhiteSpace(vm.PhenotypeKeyword))
                {
                    geneToFind = geneToFind.Where(g => g.Alleles.Any(a=> EF.Functions.Like(a.Phenotype, "%" + vm.PhenotypeKeyword + "%")));
                }

                
                
                var viewModel = new GeneSearchViewModel
                {
                    genes = await geneToFind.ToListAsync(),                    
                    SelectedGene = vm.SelectedGene,
                    GeneList = geneList,
                    LocusList = locusList,
                    AlleleList = alleleList,
                    MarkerList = markerList,
                    MutantList = mutantList,
                    ChromosomeList = chromosomeList,
                    PhenotypeCategoryList = phenoCat,
                    SelectedLocus = vm.SelectedLocus,
                    SelectedAllele = vm.SelectedAllele,
                    SelectedMarker = vm.SelectedMarker,
                    SynonymToSearch = vm.SynonymToSearch,
                    SelectedMutant = vm.SelectedMutant,
                    SelectedChromosome = vm.SelectedChromosome,
                    SelectedPhenotypeCategory = vm.SelectedPhenotypeCategory,
                    PhenotypeToSearch = vm.PhenotypeToSearch,                    
                };  
                return viewModel;


            }
            var freshModel = new GeneSearchViewModel
            {
                genes = new List<Gene>(),
                GeneList = geneList,
                LocusList = locusList,
                AlleleList = alleleList,
                MarkerList = markerList,
                MutantList = mutantList,
                ChromosomeList = chromosomeList,
                PhenotypeCategoryList = phenoCat,
            };           

            return freshModel;
        }

        
    } 
    
}