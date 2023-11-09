using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TGRC.Models
{
    public partial class Accession
    {
        [Display(Name="Accession")]
        [Key]
        public string AccessionNum { get; set; }
        public string Status { get; set; }
        [Display(Name ="Formerly")]
        public string Taxon { get; set; }
        [Display(Name ="Taxon (Solanum)")]
        public string Taxon2 { get; set; }
        [Display(Name ="Ils Number")]
        public string IlsNumber { get; set; }
        [Display(Name = "Other Identifier")]
        public string OtherId { get; set; }
        public string Traits { get; set; }
        [Display(Name ="Mating System")]
        public string MatingSystem { get; set; }
        [Display(Name ="Cultivar Name")]
        public string CultivarName { get; set; }
        [Display(Name ="Stock Increase Notes")]
        public string StockIncreaseNotes { get; set; }
        public string Comments { get; set; }
        [Display(Name ="Collection Number")]
        public string CollectionNum { get; set; }
       
        [Display(Name ="Collection Day")]
        public string CollectionDate { get; set; }
        [Display(Name ="Collection Month")]
        public string CollectionMonth { get; set; }
        [Display(Name ="Collection Year")]
        public string CollectionYear { get; set; }
        [Display(Name ="Collection Site")]        
        public string CollectionSite { get; set; }
        [Display(Name ="Site Details")]
        public string SiteDetails { get; set; }
        [Display(Name ="Province or Department")]
        public string ProvinceOrDepartment { get; set; }
        public string Country { get; set; }
        [Display(Name ="Population Size")]
        public string PopulationSize { get; set; }
        [Display(Name ="Sample Size")]
        public string SampleSize { get; set; }
        public string Habitat { get; set; }
        public string Elevation { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int ElevationInteger { get; set; }
        [Display(Name ="Associated Biota")]
        public string AssociatedBiota { get; set; }
        [Display(Name ="Vegetation Type")]
        public string VegetationType { get; set; }
        [Display(Name ="Collection Notes")]
        public string CollectionNotes { get; set; }
        [Display(Name ="Background genotype")]
        public string SourceOfAccession { get; set; }
        [Display(Name ="Karyotypical Variations")]
        public string KaryotypicalVariations { get; set; }
        [Display(Name ="Sporophytic Chromosome Number")]
        public string ChromosomeNum { get; set; }
        [Display(Name ="Relevant Chromosome")]
        public string RelevantChromosome { get; set; }
        public short? LatDeg { get; set; }
        public byte? LatMin { get; set; }
        public byte? LatSec { get; set; }
        public string LatDir { get; set; }
        public double? LatDec { get; set; }
        public short? LonDeg { get; set; }
        public byte? LonMin { get; set; }
        public byte? LonSec { get; set; }
        public string LonDir { get; set; }
        public double? LonDec { get; set; }
        [Display(Name = "Latitude/longitude source")]
        public string LatLonSource { get; set; }
        public string ElevSource { get; set; }
        [Display(Name ="Use of this accession is restricted by:")]
        public string Mta { get; set; }
        public int? CulRecId { get; set; }
        public int? DiseaseResist { get; set; }
        [Display(Name ="Year accessioned")]
        public short? AccessionYear { get; set; }
        public int? SeedToShip { get; set; }
        public DateTime? DateModified { get; set; }
        [Display(Name ="Inspected for international shipment?")]
        public bool Inspected { get; set; }
        public bool Grow { get; set; }
        public string Reference { get; set; }
        public string ReferenceOld { get; set; }
        public bool PstvdStatus { get; set; }

        public string ReferenceSafe
        {
            get
            {
                return string.IsNullOrEmpty(Reference) ? "none found" : Reference;
            }
        }


        public string LatitudeCombined { 
            get
            {
                StringBuilder lat = new StringBuilder();
                if(LatDeg != null)
                {
                    lat.Append(LatDeg + " deg. ");
                }
                if(LatMin != null)
                {
                    lat.Append(LatMin + " min ");
                }
                if(LatSec != null)
                {
                    lat.Append(LatSec + " sec ");
                }
                if(LatDir != null)
                {
                    lat.Append(LatDir + "; ");
                }
                if(LatDec != null)
                {
                    lat.Append("decimal: ");
                    lat.Append($"{LatDec:n6}");
                }
                return lat.ToString();            
            }
        }

        public string LongitudeCombined { 
            get
            {               
               StringBuilder lon = new StringBuilder();
                if(LonDeg != null)
                {
                    lon.Append(LonDeg + " deg. ");
                }
                if(LonMin != null)
                {
                    lon.Append(LonMin + " min ");
                }
                if(LonSec != null)
                {
                    lon.Append(LonSec + " sec ");
                }
                if(LonDir != null)
                {
                    lon.Append(LonDir + "; ");
                }
                if(LonDec != null)
                {
                    lon.Append("decimal: ");
                    lon.Append($"{LonDec:n6}");
                }
                return lon.ToString(); 
            }
        }

        [Display(Name = "Collection Date (m-d-y)")]
        public string CollectionDateCombined {
            get 
            {
                if(!string.IsNullOrWhiteSpace(CollectionMonth) && !string.IsNullOrWhiteSpace(CollectionDate) && !string.IsNullOrWhiteSpace(CollectionYear))
                {
                    return $"{CollectionMonth}-{CollectionDate}-{CollectionYear}";
                }
                if(!string.IsNullOrWhiteSpace(CollectionMonth) && !string.IsNullOrWhiteSpace(CollectionYear))
                {
                    return $"{CollectionMonth}-{CollectionYear}";
                }
                if(!string.IsNullOrWhiteSpace(CollectionYear))
                {
                    return $"{CollectionYear}";
                }
                return "";
            }
        }

        public string Title {
             get {
                return $"{AccessionNum} ({Taxon2})";
             }
        }

        

        [ForeignKey("AccessionNum")]
        public ICollection<DonorsInAccession> Donors { get; set; }

        [ForeignKey("AccessionNum")]
        public ICollection<AccessionCategoriesInAccession> Categories { get; set; }

        [ForeignKey("AccessionNum")]
        public ICollection<CultureInAccession> Cultures { get; set; }

        [ForeignKey("AccessionNum")]
        public ICollection<GenesAndAllelesInAccession> Genes { get; set; }
        
        [ForeignKey("AccessionNum")]
        public ICollection<AccessionsInImage> Images { get; set; }
        
        
    }
}
