using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace BusinessEntities
{
    /// <summary>
    /// Summary description for customerEntity
    /// </summary>
    public class customerEntity
    {
        public customerEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int TravellerId { get; set; }
        public string TravellerTitel { get; set; }
        public string TravellerFirstName { get; set; }
        public string TravellerLastName { get; set; }
        public string TravellerMailId { get; set; }
        public string TravellerPhone { get; set; }
        public string TravellerMobile { get; set; }
        public string TravellerAddress { get; set; }
        public string TravellerPassPortNo { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CompanyId { get; set; }
    }
}