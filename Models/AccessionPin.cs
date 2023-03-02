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
        public string LinkText { 
            get{
                if(!AccessionNum.Contains(","))
                {
                    return $"<a target=\"_blank\" href=\"/Accession/Detail/{AccessionNum}\">{AccessionNum}</a>({Taxon2}";
                }
                var numbers = AccessionNum.Split(",");
                var taxons = Taxon2.Split(",");
                StringBuilder links = new StringBuilder();
                for(var i = 0; i < numbers.Count(); i++)
                {
                    links.Append($"<a target=\"_blank\" href=\"/Accession/Detail/{numbers[i].Trim()}\">{numbers[i].Trim()}</a>({taxons[i]}");
                }
                // foreach(var num in numbers)
                // {
                //     links.Append($"<a target=\"_blank\" href=\"/Accession/Detail/{num.Trim()}\">{num.Trim()}</a>({Taxon2}");
                // }
                return links.ToString();
            }
        }

    }
}