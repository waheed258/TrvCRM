﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace BusinessEntities
{
    /// <summary>
    /// Summary description for LeadEntity
    /// </summary>
    public class LeadEntity
    {
        public LeadEntity()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int LeadID { get; set; }
        public int SourceID { get; set; }
        public string SourceRef { get; set; }
        public int? AssignedTo { get; set; }
        public int? AssignedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int ProductType { get; set; }
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public decimal Budget { get; set; }
        public string Notes { get; set; }
        public decimal QuotedPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Status { get; set; }
        public string Others { get; set; }
        public int LeadStatus { get; set; }
        public string LeadDescription { get; set; }

        public int FollowupID { get; set; }       
        public string FollowupDate { get; set; }
        public string FollowupDesc { get; set; }

        public string PackageId { get; set; }

        public string ClientFileId { get; set; }
        public string ConsultantNotes { get; set; }
        public string Reminder { get; set; }
        public string ReminderNotes { get; set; }
        public string ProductID { get; set; }
        public string WebsiteConsultantNotes { get; set; }
    }
}