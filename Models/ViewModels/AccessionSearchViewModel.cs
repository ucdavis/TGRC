using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        public string SimpleSearchTerm { get; set; }
        public string SubmitButton { get; set; }
        
        public string AccessionNumberToSearch { get; set; }

        public string StatusToSearch { get; set; }
        public string InspectedSearch { get; set; }
        public string OtherIdSearch { get; set; }
        public string SelectedTaxon { get; set; }
        public List<Taxa> Taxons { get; set; }
        public string CultivarToSearch { get; set; }
        public List<string> Cultivars { get; set; }
        public string CategoryToSearch { get; set; }
        public List<string> Categories { get; set; }
        public string SelectedCountry { get; set; }
        public List<string> Countries { get; set; }
        public string SearchProvince { get; set; }
        public List<string> ProvinceList { get; set; }
        public string SearchCollectionSite { get; set; }
        public string SearchComments { get; set; }
        public string SelectedMatingSystem { get; set; }
        public List<string> MattingSystems { get; set; }
        public string SelectedGene { get; set; }
        public List<string> GeneList { get; set; }
        public string SelectedBackgroundGenotype { get; set; }
        public List<string> BackgroundGenotypeList { get; set; }
        

        public AccessionSearchViewModel() {
            Search = false;
            StatusToSearch = "Active";
        }
        
        public static async Task<AccessionSearchViewModel> Create(TGRCContext _context, AccessionSearchViewModel vm)
        {   
           var taxa = await _context.Taxa.OrderByDescending(t => t.L).ThenBy(t => t.Taxon).ToListAsync();
           taxa.Insert(0, new Taxa { Taxon = "", CompleteName = "", Synonym = "", L="z"});
           var cultivars = await _context.Accessions.Where(a => a.CultivarName != null).OrderBy(a => a.CultivarName).Select(a => a.CultivarName).Distinct().ToListAsync();
           cultivars.Insert(0, "");
           var cat = await _context.AccessionCategories.Select(c => c.AccessionCategory).Distinct().ToListAsync();
           cat.Insert(0, "");
           var countries = await _context.Accessions.Where(a => a.Country != null).Select(a => a.Country).Distinct().OrderBy( a=> a).ToListAsync();           
           countries.Insert(0,"");
           var provinces = await _context.Accessions.Where(a => a.ProvinceOrDepartment != null).Select(a => a.ProvinceOrDepartment).Distinct().OrderBy(a=>a).ToListAsync();
           provinces.Insert(0,"");
           var matingList = await _context.MatingSystems.Select(a =>a.System).OrderBy(a => a).ToListAsync();
           matingList.Insert(0,"");
           var geneList = await _context.GenesAndAllelesInAccessions.Select(a => a.Gene).Distinct().OrderBy(a=>a).ToListAsync();
           geneList.Insert(0,"");
           var genotypeList = await _context.Accessions.Where(a => a.SourceOfAccession != null).Select(a => a.SourceOfAccession).Distinct().OrderBy(a=>a).ToListAsync();
           genotypeList.Insert(0,"");

                              
            if(vm != null)
            {
                var accToFind = _context.Accessions.AsQueryable();

                if(vm.SubmitButton == "Simple")
                {
                    var accPad = AccessionSearchViewModel.padAccessionSearch(vm.SimpleSearchTerm);
                    var accFuzzy = fuzzySearch(vm.SimpleSearchTerm);
                    accToFind = accToFind.Where(a => a.Status == "Active" && (EF.Functions.Like(a.AccessionNum, "%" + vm.SimpleSearchTerm + "%") 
                        || EF.Functions.Like(a.AccessionNum, "%" + accPad + "%")
                        || EF.Functions.Like(a.AccessionNum, "%" + accFuzzy + "%")
                        || EF.Functions.Like(a.OtherId, "%" + vm.SimpleSearchTerm + "%") 
                        || EF.Functions.Like(a.OtherId, "%" + accPad + "%")
                        || EF.Functions.Like(a.OtherId, "%" + accFuzzy + "%")
                        || EF.Functions.Like(a.CultivarName, "%" + vm.SimpleSearchTerm + "%") 
                        || EF.Functions.Like(a.CultivarName, "%" + accPad + "%")
                        || EF.Functions.Like(a.CultivarName, "%" + accFuzzy + "%")
                        || EF.Functions.Like(a.Reference, "%" + vm.SimpleSearchTerm + "%") 
                        || EF.Functions.Like(a.Reference, "%" + accPad + "%")
                        || EF.Functions.Like(a.Reference, "%" + accFuzzy + "%")));
                } else {
                
                    if(!string.IsNullOrWhiteSpace(vm.AccessionNumberToSearch))
                    {
                        var accPad = padAccessionSearch(vm.AccessionNumberToSearch);
                        accToFind = accToFind.Where(a => EF.Functions.Like(a.AccessionNum, "%" + vm.AccessionNumberToSearch + "%") || EF.Functions.Like(a.AccessionNum, "%" + accPad + "%"));
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
                        accToFind = accToFind.Where(a => EF.Functions.Like(a.OtherId, "%" + vm.OtherIdSearch + "%") || EF.Functions.Like(a.CollectionNum, "%" + vm.OtherIdSearch + "%"));
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SelectedTaxon))
                    {
                        accToFind = accToFind.Where(a => a.Taxon2 == vm.SelectedTaxon || a.Taxon == vm.SelectedTaxon);
                    }
                    if(!string.IsNullOrWhiteSpace(vm.CultivarToSearch))
                    {
                        accToFind = accToFind.Where(a => a.CultivarName == vm.CultivarToSearch);
                    }
                    if(!string.IsNullOrWhiteSpace(vm.CategoryToSearch))
                    {
                        accToFind = accToFind.Where(a => a.Categories.Any(c => c.AccessionCategory == vm.CategoryToSearch));
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SelectedCountry))
                    {
                        accToFind = accToFind.Where(a => a.Country == vm.SelectedCountry);
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SearchProvince))
                    {
                        accToFind = accToFind.Where(a => a.ProvinceOrDepartment == vm.SearchProvince);
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SearchCollectionSite))
                    {
                        accToFind = accToFind.Where(a => EF.Functions.Like(a.CollectionSite, "%" + vm.SearchCollectionSite + "%"));
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SearchComments))
                    {
                        accToFind = accToFind.Where(a => EF.Functions.Like(a.Comments, "%" + vm.SearchComments + "%") || EF.Functions.Like(a.CollectionNotes, "%" + vm.SearchComments + "%") || EF.Functions.Like(a.Traits, "%" + vm.SearchComments + "%"));
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SelectedMatingSystem))
                    {
                        accToFind = accToFind.Where(a => a.MatingSystem == vm.SelectedMatingSystem);
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SelectedGene))
                    {
                        accToFind = accToFind.Where(a => a.Genes.Any(g => g.Gene == vm.SelectedGene));
                    }
                    if(!string.IsNullOrWhiteSpace(vm.SelectedBackgroundGenotype))
                    {
                        accToFind = accToFind.Where(a => a.SourceOfAccession == vm.SelectedBackgroundGenotype);
                    }
                }
                
                
                var viewModel = new AccessionSearchViewModel
                {
                    accessions = await accToFind.OrderBy(a => a.AccessionNum).ToListAsync(),
                    Taxons = taxa,
                    Cultivars = cultivars,
                    Categories = cat,
                    Countries = countries,
                    ProvinceList = provinces,
                    MattingSystems = matingList,
                    GeneList = geneList,
                    BackgroundGenotypeList = genotypeList,
                    AccessionNumberToSearch = vm.AccessionNumberToSearch,
                    StatusToSearch = vm.StatusToSearch,
                    InspectedSearch = vm.InspectedSearch,
                    OtherIdSearch = vm.OtherIdSearch,
                    SelectedTaxon = vm.SelectedTaxon,
                    CultivarToSearch = vm.CultivarToSearch,
                    CategoryToSearch = vm.CategoryToSearch,
                    SelectedCountry = vm.SelectedCountry,
                    SearchCollectionSite = vm.SearchCollectionSite,
                    SearchProvince = vm.SearchProvince,
                    SearchComments = vm.SearchComments,
                    SelectedMatingSystem = vm.SelectedMatingSystem,
                    SelectedGene = vm.SelectedGene,
                    SelectedBackgroundGenotype = vm.SelectedBackgroundGenotype,
                    SimpleSearchTerm = vm.SimpleSearchTerm,
                    Search = true,
                };  
                return viewModel;


            }
            var freshModel = new AccessionSearchViewModel
            {
                accessions = new List<Accession>(),  
                Taxons = taxa,       
                Cultivars = cultivars,
                Categories = cat,  
                Countries = countries,
                ProvinceList = provinces,
                MattingSystems = matingList,    
                BackgroundGenotypeList = genotypeList,
                GeneList = geneList,
                Search = false,
            };           

            return freshModel;
        }

        public static string padAccessionSearch(string search)
        {
            var newSearch = "IgnoreThisSearch";
            if(search.IndexOf("la", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int a;
                if(int.TryParse(search.Replace("LA","").Replace("La","").Replace("la","").Replace("lA",""), out a))
                {
                    newSearch = "LA" + a.ToString("D4");
                    return newSearch;
                }                
            }
            if(search.Contains("2-"))
            {
                int b;
                if(int.TryParse(search.Replace("2-",""), out b))
                {
                    newSearch = "2-" + b.ToString("D3");
                    return newSearch;
                }
            }
            if(search.Contains("3-"))
            {
                int b;
                if(int.TryParse(search.Replace("3-",""), out b))
                {
                    newSearch = "3-" + b.ToString("D3");
                    return newSearch;
                }
            }
             if(search.IndexOf("delta", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int a;
                if(int.TryParse(search.Replace("DELTA","").Replace("Delta","").Replace("delta","").Replace("-",""), out a))
                {
                    newSearch = "delta-" + a.ToString("D2");
                    return newSearch;
                }                
            }
            return newSearch;
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