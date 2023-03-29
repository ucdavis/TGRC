using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class PhenoInGene
    {
        public string Gene { get; set; }
        public string PhenotypicalCategory { get; set; }
        public string Allele { get; set; }
        public string PrimaryPhenotype { get; set; }
        public DateTime? DateModified { get; set; }

        [ForeignKey("PhenotypicalCategory")]
        public PhenotypicCategory Category { get; set; }

        // [ForeignKey("Gene, Allele")]
        // public GenesAndAllelesInAccession GeneInAccession { get; set; }
    }
}
