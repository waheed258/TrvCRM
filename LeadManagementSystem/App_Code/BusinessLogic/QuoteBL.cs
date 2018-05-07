using DataLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

    public DataSet GetQuotePDFData(int leadID)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("@leadID", leadID);
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GenerateQuotePDF", hashtable);
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
        hashtable.Add("@AdultTotal", quoteEntity.AdultTotal);
        hashtable.Add("@ChildTotal", quoteEntity.ChildTotal);
        hashtable.Add("@IsMailSent", quoteEntity.IsMailSent);
        hashtable.Add("@LeadID", quoteEntity.LeadID);
        if (quoteEntity.QuoteDate != "")
        {
            hashtable.Add("@QuoteDate", DateTime.ParseExact(quoteEntity.QuoteDate, "dd-MM-yyyy", null));
        }
        else
        {
            hashtable.Add("@QuoteDate", DBNull.Value);
        }
        hashtable.Add("@Operation", quoteEntity.Operation);
        hashtable.Add("@QuoteNumber", quoteEntity.QuoteNumber);
        int result = dataUtilities.ExecuteNonQuery("usp_CUDQuote", hashtable);
        return result;
    }

    public string CUOperationQuote(QuoteEntity quoteEntity)
    {
        string strQuoteNumber = string.Empty;
        try
        {
            DataUtilities dtUtil = new DataUtilities();

            string strConnection = dtUtil.GetConnectionString();            

            using (SqlConnection con = new SqlConnection(strConnection))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CUDQuote", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ToCity", quoteEntity.ToCity);
                    cmd.Parameters.AddWithValue("@FlightDetails", quoteEntity.FlightDetails);
                    cmd.Parameters.AddWithValue("@CarHireDetails", quoteEntity.CarHireDetails);
                    cmd.Parameters.AddWithValue("@HotelInfo", quoteEntity.HotelInfo);
                    cmd.Parameters.AddWithValue("@ItineraryDetails", quoteEntity.ItineraryDetails);
                    cmd.Parameters.AddWithValue("@Includes", quoteEntity.Includes);
                    cmd.Parameters.AddWithValue("@Excludes", quoteEntity.Excludes);
                    cmd.Parameters.AddWithValue("@CostForAdultType", quoteEntity.CostForAdultType);
                    cmd.Parameters.AddWithValue("@CostForAdult", quoteEntity.CostForAdult);
                    cmd.Parameters.AddWithValue("@NoOfAdults", quoteEntity.NoOfAdults);
                    cmd.Parameters.AddWithValue("@CostForChildType", quoteEntity.CostForChildType);
                    cmd.Parameters.AddWithValue("@CostForChild", quoteEntity.CostForChild);
                    cmd.Parameters.AddWithValue("@NoOfChildren", quoteEntity.NoOfChildren);
                    cmd.Parameters.AddWithValue("@TravelInsurance", quoteEntity.TravelInsurance);
                    cmd.Parameters.AddWithValue("@ConsultantName", quoteEntity.ConsultantName);
                    cmd.Parameters.AddWithValue("@AdultTotal", quoteEntity.AdultTotal);
                    cmd.Parameters.AddWithValue("@ChildTotal", quoteEntity.ChildTotal);
                    cmd.Parameters.AddWithValue("@IsMailSent", quoteEntity.IsMailSent);
                    cmd.Parameters.AddWithValue("@LeadID", quoteEntity.LeadID);

                    if (quoteEntity.QuoteDate != "")
                    {
                        cmd.Parameters.AddWithValue("@QuoteDate", DateTime.ParseExact(quoteEntity.QuoteDate, "dd-MM-yyyy", null));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@QuoteDate", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@Operation", quoteEntity.Operation);
                    cmd.Parameters.AddWithValue("@QuoteNumber", quoteEntity.QuoteNumber);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 15);
                    cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    strQuoteNumber = cmd.Parameters["@ReturnValue"].Value.ToString();
                }
            }
        }
        catch 
        {  }
        

        return strQuoteNumber;
    }



}