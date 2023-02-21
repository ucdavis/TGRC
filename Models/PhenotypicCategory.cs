using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class PhenotypicCategory
    {
        public string PhenotypicCategory1 { get; set; }
        public string Description { get; set; }
        public string AbbrevForSolgenes { get; set; }

        public string FullName
        {
            get
            {
                if(PhenotypicCategory1 == "")
                {
                    return "";
                }
                return $"{PhenotypicCategory1}, {Description}";
            }
        }
    }
}
