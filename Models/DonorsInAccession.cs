using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class DonorsInAccession
    {
        public int? ColleagueNum { get; set; }
        public string AccessionNum { get; set; }
        public bool? Donor { get; set; }
        public bool? Collector { get; set; }
    }
}
