using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class GenesAndAllelesInImage
    {
        public int? ImageNum { get; set; }
        public string Gene { get; set; }
        public string Allele { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
