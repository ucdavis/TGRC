using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class AccessionsInImage
    {
        public int? ImageNum { get; set; }
        public string AccessionNum { get; set; }
        public DateTime? DateModified { get; set; }

        public Image Image { get; set; }
    }
}
