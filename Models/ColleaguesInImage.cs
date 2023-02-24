using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class ColleaguesInImage
    {
        public int ImageNum { get; set; }
        public int ColleagueNum { get; set; }
        public DateTime? DateModified { get; set; }

        [ForeignKey("ImageNum")]
        public Image Image { get; set; }
    }
}
