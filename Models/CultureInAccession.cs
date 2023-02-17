using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGRC.Models
{
    public partial class CultureInAccession
    {
        public string AccessionNum { get; set; }
        public int RecommendationNum { get; set; }

        [ForeignKey("RecommendationNum")]
        public CulturalRecommendation Recommendation { get; set; }
    }
}
