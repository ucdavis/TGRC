using System;
using System.Collections.Generic;

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
    }
}
