using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TGRC.Models
{
    public partial class AccessionPin
    {
        public string AccessionNum { get; set; }
        public string Taxon2 { get; set; }
        public double LatDec { get; set; }
        public double LonDec { get; set; }

        public string Title {get; set;}
        public string Icon { get; set; }
        public string Reference { get; set; }
        public string LinkText { 
            get{
                if(!AccessionNum.Contains(","))
                {
                    return $"<a target=\"_blank\" href=\"/Accession/Detail/{AccessionNum}\">{AccessionNum}</a> {Taxon2} <br>{Reference}";
                }
                var numbers = AccessionNum.Split(",");
                var taxons = Taxon2.Split(",");
                var refText = Reference.Split(",");
                StringBuilder links = new StringBuilder();
                for(var i = 0; i < numbers.Count(); i++)
                {
                    if(i < numbers.Count() - 1){
                        links.Append($"<a target=\"_blank\" href=\"/Accession/Detail/{numbers[i].Trim()}\">{numbers[i].Trim()}</a> {taxons[i]} <br>");
                    } else {
                        links.Append($"<a target=\"_blank\" href=\"/Accession/Detail/{numbers[i].Trim()}\">{numbers[i].Trim()}</a> {taxons[i]} <br>{refText[i]}");
                    }
                    
                }                
                return links.ToString();
            }
        }

    }
}