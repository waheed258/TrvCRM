using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLogic;
using BusinessEntities;
using System.Data;
using System.Collections;

namespace BusinessLogic
{
    /// <summary>
    /// Summary description for LeadBL
    /// </summary>
    public class LeadBL
    {
        DataUtilities dataUtilities = new DataUtilities();
        public LeadBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataSet GetLeadsList(int ID)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@lsID", ID);
            DataSet ds = dataUtilities.ExecuteDataSet("usp_GetAllLeads", hashtable);
            return ds;
        }
        public int CUDLead(LeadEntity leadEntity, char Operation)
        {
            Hashtable hashtable = new Hashtable();
            if (Operation == 'I')
            {
                hashtable.Add("@lsId", 0);
                hashtable.Add("@lsCreatedBy",leadEntity.CreatedBy);
                hashtable.Add("@lsUpdatedBy", leadEntity.UpdatedBy);

            }
            else
            {
                hashtable.Add("@lsId", leadEntity.LeadID);
                hashtable.Add("@lsCreatedBy", 0);
                hashtable.Add("@lsUpdatedBy", leadEntity.UpdatedBy);
                
            }
            if (leadEntity.FollowupDate != "")
            {
                hashtable.Add("@FollowupDate", DateTime.ParseExact(leadEntity.FollowupDate, "dd-MM-yyyy", null));
            }
            else {
                hashtable.Add("@FollowupDate", DBNull.Value);
            }
            hashtable.Add("@FollowupDesc", leadEntity.FollowupDesc);
            hashtable.Add("@lsAssignedTo", leadEntity.AssignedTo);
            hashtable.Add("@lsAssignedBy", leadEntity.AssignedBy);
            hashtable.Add("@lsOthersInfo", leadEntity.Others);
            hashtable.Add("@lsSource", leadEntity.SourceID);
            hashtable.Add("@lsSourceRef", leadEntity.SourceRef);
            hashtable.Add("@lsFirstName", leadEntity.FirstName);
            hashtable.Add("@lsLastName", leadEntity.LastName);
            hashtable.Add("@lsMobile", leadEntity.Mobile);
            hashtable.Add("@lsEmailID", leadEntity.Email);
            hashtable.Add("@lsProdType", leadEntity.ProductType);
            hashtable.Add("@lsOriginName", leadEntity.OriginName);
            hashtable.Add("@lsDestinationName", leadEntity.DestinationName);
            hashtable.Add("@lsLeadDesc", leadEntity.LeadDescription);

            if (leadEntity.DepartureDate != "")
            {
                hashtable.Add("@lsDepartureDate", DateTime.ParseExact(leadEntity.DepartureDate, "dd-MM-yyyy", null));
            }
            else
            {
                hashtable.Add("@lsDepartureDate", DBNull.Value);
            }
            if (leadEntity.ReturnDate != "")
            {
                hashtable.Add("@lsReturnDate", DateTime.ParseExact(leadEntity.ReturnDate, "dd-MM-yyyy", null));
            }
            else
            {
                hashtable.Add("@lsReturnDate", DBNull.Value);
            }
            hashtable.Add("@lsLeadStatus", leadEntity.LeadStatus);
            hashtable.Add("@lsAdults", leadEntity.Adult);
            hashtable.Add("@lsChildren", leadEntity.Child);
            hashtable.Add("@lsInfants", leadEntity.Infant);
            hashtable.Add("@lsBudget", leadEntity.Budget);
            hashtable.Add("@lsNotes", leadEntity.Notes);
            hashtable.Add("@lsQuotedPrice", leadEntity.QuotedPrice);
            hashtable.Add("@lsFinalPrice", leadEntity.FinalPrice);
            hashtable.Add("@Operation", Operation);
            hashtable.Add("@lsPackageId", leadEntity.PackageId);

            int result = dataUtilities.ExecuteNonQuery("usp_CUDLead", hashtable);
            return result;
        }


        public int LeadAction(LeadEntity leadEntity)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@AssignedTo", leadEntity.AssignedTo);
            hashtable.Add("@AssignedBy", leadEntity.AssignedBy);
            hashtable.Add("@LeadStatus", leadEntity.LeadStatus);
            hashtable.Add("@lsId", leadEntity.LeadID);

            int result = dataUtilities.ExecuteNonQuery("usp_LeadActions", hashtable);
            return result;
        }

        public DataSet GetProduct()
        {
            DataSet ds = dataUtilities.ExecuteDataSet("GetProduct");
            return ds;
        }

        public DataSet GetMailInfo()
        {
            DataSet ds = dataUtilities.ExecuteDataSet("GetConfig");
            return ds;
        }

        public DataSet GetSourceData(string Operation)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@Operation", Operation);
            DataSet ds = dataUtilities.ExecuteDataSet("usp_GetSourceType", hashtable);
            return ds;
        }

        public DataSet GetFollowupCount(int LeadID)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@LeadID", LeadID);
            DataSet ds = dataUtilities.ExecuteDataSet("usp_GetFollowupCount", hashtable);
            return ds;
        }
       
    }
}