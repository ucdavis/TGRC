using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class PhenoInGene
    {
        public string Gene { get; set; }
        public string PhenotypicalCategory { get; set; }
        public string Allele { get; set; }
        public string PrimaryPhenotype { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
