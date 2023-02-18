using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class Taxa
    {
        public string Taxon { get; set; }
        public string CompleteName  { get; set; }
        public string Label { get; set; }
        public string Synonym { get; set; }
        public string Authority { get; set; }
        public string SpeciesAbbreviation { get; set; }
        public string L { get; set; }

        public string Display
        {
            get{
                if(CompleteName == "")
                {
                    return "";
                }
                return $"{CompleteName} ({Synonym})";
            }
        }
        
    }
}
