using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class AccessionsInImage
    {
        public int ImageNum { get; set; }
        public string AccessionNum { get; set; }
        public DateTime? DateModified { get; set; }

        [ForeignKey("ImageNum")]
        public Image Image { get; set; }
    }
}
