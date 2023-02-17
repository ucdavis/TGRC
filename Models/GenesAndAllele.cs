﻿using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class GenesAndAllele
    {
        public string Gene { get; set; }
        public string Allele { get; set; }
        public string AlleleName { get; set; }
        public string SynonymsOfAllele { get; set; }
        public string MutantType { get; set; }
        public string Phenotype { get; set; }
        public string Notes { get; set; }
        public string PhenotypeCombo { get; set; }
        public DateTime? DateModified { get; set; }
    }
}