using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class AccessionCategoriesInAccession
    {
        public string AccessionNum { get; set; }
        public string AccessionCategory { get; set; }        
    }
}
