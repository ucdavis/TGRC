using System;
using System.Collections.Generic;

namespace TGRC.Models
{
    public partial class Colleague
    {
        public int? ColleagueNum { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Associate { get; set; }
        public string Institute { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Status { get; set; }
        public string WebAddress { get; set; }
        public DateTime? DateModified { get; set; }
        public string CountryGroup { get; set; }
        public string CoName { get; set; }
        public string SInstitute { get; set; }
        public string SAddress1 { get; set; }
        public string SAddress2 { get; set; }
        public string SCity { get; set; }
        public string SState { get; set; }
        public string SPostalCode { get; set; }
        public string SCountry { get; set; }
        public string User { get; set; }
        public string UsdaOrganizationalCategory { get; set; }
        public string Salutation { get; set; }
    }
}
