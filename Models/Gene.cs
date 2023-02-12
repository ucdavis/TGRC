using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class Gene
    {
        public string Gene1 { get; set; }
        public string LocusName { get; set; }
        public string Chromosome { get; set; }
        public string Arm { get; set; }
        public string MarkerType { get; set; }
        public string Notes { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
