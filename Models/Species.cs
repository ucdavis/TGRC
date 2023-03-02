using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TGRC.Models
{
    public partial class Species
    {
        [Key]
        public string Taxon { get; set; }
        public string Icon { get; set; }
    }
}