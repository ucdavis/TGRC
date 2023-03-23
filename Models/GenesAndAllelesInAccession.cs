using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class GenesAndAllelesInAccession
    {
        public string AccessionNum { get; set; }
        public string Gene { get; set; }
        public string Allele { get; set; }
        public string Isogenicity { get; set; }
        public bool? PrimarySource { get; set; }
        public string Background { get; set; }
        public DateTime? DateModified { get; set; }

        public string Display { 
            get {
                if(Allele == "--")
                {
                    return Gene;
                }
                return $"{Gene}^{Allele}";                
            }
        }

        public Accession Accession { get; set; }

        [ForeignKey("Gene,Allele")]
        public ICollection<PhenoInGene> Phenotypes { get; set; }
    }
}
