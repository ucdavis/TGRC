using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGRC.Models
{
    public partial class RequestSummary
    {
        [Key]
        public string SampleNum { get; set; }
        public int RequestedCount { get; set; }
        public string Taxon { get; set; }
        public string Reference { get; set; }
        public int TotalRequests { get; set; }
    }
}