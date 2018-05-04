using DataLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QuoteBL
/// </summary>
public class QuoteBL
{
    DataUtilities dataUtilities = new DataUtilities();
    
    public DataSet GetTypeData(string Type)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("@Type", Type);
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetCostType", hashtable);
        return ds;
    }

    public DataSet GetIncudeExcludes()
    {
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetIncules_Excludes");
        return ds;
    }

    public int CUDQuote(QuoteEntity quoteEntity)
    {
        Hashtable hashtable = new Hashtable();
        
        hashtable.Add("@ToCity", quoteEntity.ToCity);
        hashtable.Add("@FlightDetails", quoteEntity.FlightDetails);
        hashtable.Add("@CarHireDetails", quoteEntity.CarHireDetails);
        hashtable.Add("@HotelInfo", quoteEntity.HotelInfo);
        hashtable.Add("@ItineraryDetails", quoteEntity.ItineraryDetails);
        hashtable.Add("@Includes", quoteEntity.Includes);
        hashtable.Add("@Excludes", quoteEntity.Excludes);
        hashtable.Add("@CostForAdultType", quoteEntity.CostForAdultType);
        hashtable.Add("@CostForAdult", quoteEntity.CostForAdult);
        hashtable.Add("@NoOfAdults", quoteEntity.NoOfAdults);
        hashtable.Add("@CostForChildType", quoteEntity.CostForChildType);
        hashtable.Add("@CostForChild", quoteEntity.CostForChild);
        hashtable.Add("@NoOfChildren", quoteEntity.NoOfChildren);
        hashtable.Add("@TravelInsurance", quoteEntity.TravelInsurance);
        hashtable.Add("@ConsultantName", quoteEntity.ConsultantName);
        hashtable.Add("@LeadID", quoteEntity.LeadID);
        if (quoteEntity.QuoteDate != "")
        {
            hashtable.Add("@QuoteDate", DateTime.ParseExact(quoteEntity.QuoteDate, "dd-MM-yyyy", null));
        }
        else
        {
            hashtable.Add("@QuoteDate", DBNull.Value);
        }


        int result = dataUtilities.ExecuteNonQuery("usp_CUDQuote", hashtable);
        return result;
    }
}