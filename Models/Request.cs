using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class Request
    {
        [Key]
        public int RequestNum { get; set; }
        public int ColleagueNum { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string Purpose { get; set; }
        public int? NofSamples { get; set; }
        public string RequestMadeBy { get; set; }
        public string RequestFilledBy { get; set; }
        public string RequestMadeBy_status { get; set; }
        public string RequestFilledBy_stataus { get; set; }
        public string ShippedVia { get; set; }
        public DateTime DateModified { get; set; }
        public string Notes { get; set; }

        [ForeignKey("RequestNum")]
        public ICollection<AccessionInRequests> RequestComponents { get; set; }
    }
}
