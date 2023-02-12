using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public class Accessions 
    {

        //Hand built by james
        public string AccessionNum { get; set; }
        
        public string Status { get; set; }
        public string Taxon { get; set; }
        public string Taxon2 { get; set; }
        public string ILs_number { get; set; }  
        public string OtherID { get; set; }
        public string Traits { get; set; }
        public string MatingSystem { get; set; }
        public string CultivarName { get; set; }
        public string StockIncreaseNotes { get; set; }
        public string Comments { get; set; }
        public string CollectionNum { get; set; }
        public string CollectionDate { get; set; }
        public string CollectionMonth { get; set; }
        public string CollectionYear { get; set; }
        public string CollectionSite { get; set; }
        public string SiteDetails { get; set; }
        public string ProvinceOrDepartment { get; set; }
        public string Country { get; set; }
        public string PopulationSize { get; set; }
        public string SampleSize { get; set; }
        public string Habitat { get; set; }
        public string Elevation { get; set; }
        public string AssociatedBiota { get; set; }
        public string VegetationType { get; set; }
        public string CollectionNotes { get; set; }
        public string SourceOfAccession { get; set; }
        public string KaryotypicalVariations { get; set; }
        public string ChromosomeNum { get; set; }
        public string RelevantChromosome { get; set; }
        public Int16 LatDeg { get; set; }
        public Byte LatMin { get; set; }
        public Byte LatSec { get; set; }
        public string LatDir { get; set; }
        public Double LatDec { get; set; }
        public Int16 LonDeg { get; set; }
        public Byte LonMin { get; set; }
        public Byte LonSec { get; set; }
        public string LonDir { get; set; }
        public Double LonDec { get; set; }
        public string LatLonSource { get; set; }
        public string ElevSource { get; set; }
        public string MTA { get; set; }
        public int CulRecID { get; set; }
        public int DiseaseResist { get; set; }
        public Int16 AccessionYear { get; set; }
        public int SeedToShip { get; set; }
        public DateTime DateModified { get; set; }
        public bool Inspected { get; set; }
        public bool Grow { get; set; }
        public string Reference { get; set; }
        public string Reference_old { get; set; }
        public bool pstvd_status { get; set; }

    }
}