using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataManager;
using System.Data;
using System.Collections;
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
            //need to change updated by after session createds
            hashtable.Add("@lsCreatedBy", 0);

        }
        else
        {
            hashtable.Add("@lsId", leadEntity.LeadID);
            hashtable.Add("@lsCreatedBy", 0);
        }

        hashtable.Add("@lsSource", leadEntity.SourceID);
        hashtable.Add("@lsSourceRef", leadEntity.SourceRef);
        hashtable.Add("@lsFirstName", leadEntity.FirstName);
        hashtable.Add("@lsLastName", leadEntity.LastName);
        hashtable.Add("@lsMobile", leadEntity.Mobile);
        hashtable.Add("@lsEmailID", leadEntity.Email);
        hashtable.Add("@lsProdType", leadEntity.ProductType);
        hashtable.Add("@lsOriginName", leadEntity.OriginName);
        hashtable.Add("@lsDestinationName", leadEntity.DestinationName);
        if (leadEntity.DepartureDate != "")
        {
            hashtable.Add("@lsDepartureDate", DateTime.ParseExact(leadEntity.DepartureDate, "dd-MM-yyyy", null));
        }
        else {
            hashtable.Add("@lsDepartureDate", DBNull.Value);
        }
        if (leadEntity.ReturnDate != "")
        {
            hashtable.Add("@lsReturnDate", DateTime.ParseExact(leadEntity.ReturnDate, "dd-MM-yyyy", null));
        }
        else {
            hashtable.Add("@lsReturnDate", DBNull.Value);
        }
        hashtable.Add("@lsAdults", leadEntity.Adult);
        hashtable.Add("@lsChildren", leadEntity.Child);
        hashtable.Add("@lsInfants", leadEntity.Infant);
        hashtable.Add("@lsBudget", leadEntity.Budget);
        hashtable.Add("@lsNotes", leadEntity.Notes);
        hashtable.Add("@lsQuotedPrice", leadEntity.QuotedPrice);
        hashtable.Add("@lsFinalPrice", leadEntity.FinalPrice);        
        hashtable.Add("@Operation", Operation);

        int result = dataUtilities.ExecuteNonQuery("usp_CUDLead", hashtable);
        return result;
    }
    public DataSet GetProduct()
    {
        DataSet ds = dataUtilities.ExecuteDataSet("GetProduct");
        return ds;
    }
}