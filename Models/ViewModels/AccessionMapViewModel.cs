using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Text;

namespace TGRC.Models
{  
    public class CheckboxListString
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public string Icon { get; set; }
    } 
    
    public class AccessionMapViewModel
    {
        public List<Accession> accessions { get; set; }

        public bool Search { get; set; }
        public string StatusSelected { get; set; }
        
        public string CountrySelected { get; set; }
        public List<string> ProvinceList { get; set; }
        public string ProvinceSelected { get; set; }
        public string Markers { get; set; }
        public int MarkerCount { get; set; }
        public List<CheckboxListString> Species { get; set; }
        
        public int? minElevation { get; set; }
        public int? maxElevation { get; set; }
        
        

        public AccessionMapViewModel() {
            Search = false;
            CountrySelected = "[Any]";
            ProvinceSelected = "[Any]";
            StatusSelected = "Active";
        }
        
        public static async Task<AccessionMapViewModel> Create(TGRCContext _context, AccessionMapViewModel vm)
        {             
            var provinces = await _context.Accessions.Where(a=> a.ProvinceOrDepartment != null).Select(a => a.ProvinceOrDepartment).Distinct().OrderBy(a=>a).ToListAsync();
            provinces.Insert(0,"[Any]");

            vm.ProvinceList = provinces;
            if(vm.Species == null)
            {
                vm.Species = await _context.Species.Select(s => new CheckboxListString { Name = s.Taxon, IsChecked = false, Icon = s.Icon}).OrderBy(a=>a.Name).ToListAsync();
            }

            if(vm.Search)
            {

                var accList = _context.Accessions.Where(a => a.LatDec.HasValue && a.LonDec.HasValue).AsQueryable();

                if(vm.CountrySelected != "[Any]")
                {
                    accList = accList.Where(a => a.Country == vm.CountrySelected);
                } 
                if(vm.ProvinceSelected != "[Any]")
                {
                    accList = accList.Where(a => a.ProvinceOrDepartment == vm.ProvinceSelected);
                }
                if(vm.StatusSelected != "[Any]")
                {
                    accList = accList.Where(a => a.Status == vm.StatusSelected);
                }
                if(vm.Species.Any(s => s.IsChecked))
                {
                    var selectedSpecies = vm.Species.Where(s => s.IsChecked).Select(s => s.Name).ToList();
                    accList = accList.Where(a => selectedSpecies.Contains(a.Taxon2));
                    //accList = accList.Where(a => vm.Species.Where(s => s.IsChecked).Select(s => s.Name).Contains(a.Taxon2));
                }
                if(vm.minElevation != null && vm.minElevation != 0)
                {                
                    accList = accList.Where(a => EF.Functions.IsNumeric(a.Elevation) && a.ElevationInteger >= vm.minElevation);
                }
                if(vm.maxElevation != null && vm.maxElevation != 4000)
                {
                    accList = accList.Where(a => EF.Functions.IsNumeric(a.Elevation) && a.ElevationInteger <= vm.maxElevation);
                }
                
                vm.accessions = await accList.ToListAsync(); 
                var groupList = vm.accessions.GroupBy(a => new {a.LatDec, a.LonDec}).Select(g => new AccessionPin { 
                    LatDec = g.Key.LatDec.Value,
                    LonDec = g.Key.LonDec.Value,
                    AccessionNum = string.Join(", ", g.Select(a => a.AccessionNum)), 
                    Taxon2 = string.Join(", ", g.Select(a => a.Taxon2)),
                    Reference = string.Join(", ", g.Select(a => a.ReferenceSafe.Replace("'","&#39").Replace(",","&#44"))),
                    Title = string.Join(", ", g.Select(a => $"{a.AccessionNum}({a.Taxon2})")),
                    Icon = g.All(a => a.Taxon2 == g.First().Taxon2) ? g.First().Taxon2 : "multi" 
                    }).ToList();
                
                vm.MarkerCount = vm.accessions.Count();
                StringBuilder marker = new StringBuilder();  
                groupList.ForEach(a =>  marker.Append($"['{a.LinkText}', {a.LatDec}, {a.LonDec}, '{a.Icon}','{a.Reference}'],"));  
                if(marker.Length > 2)
                {
                    marker.Remove(marker.Length - 1,1);
                }                      
                vm.Markers = marker.ToString();
            } else
            {
                vm.accessions = new List<Accession>();
                //vm.minElevation = 0;
                //vm.maxElevation = 4000;
            }
                
            return vm;
           
        }

        
    } 
    
}