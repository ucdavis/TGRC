using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class DonorsInAccession
    {
        public int ColleagueNum { get; set; }
        public string AccessionNum { get; set; }
        public bool Donor { get; set; }
        public bool Collector { get; set; }

        

        [ForeignKey("ColleagueNum")]
        public Colleague Colleague { get; set; }
    }
}
