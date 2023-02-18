using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class Image
    {        
        public int ImageNum { get; set; }
        public string Caption { get; set; }
        public string Hyperlink { get; set; }
        public string File { get; set; }
        public string SlideNum { get; set; }
        public string Location { get; set; }
        public string Pedigree { get; set; }
        public short? ImageDate { get; set; }
        public int Web { get; set; }
        public DateTime? DateModified { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public short? Chk { get; set; }
        public int? Chkthumb { get; set; }
       
    }
}
