using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class AccessionInRequests
    {
        public int RequestNum { get; set; }
        public string SampleNum { get; set; }
        public string PedigreeNum { get; set; }
        public int Seed_LotsID { get; set; }
        public int Seed_StorageID { get; set; }
        public bool PrintSample { get; set; }
        public int SeedsSent { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("SampleNum")]
        public Accession Accession { get; set; }
    }
}
