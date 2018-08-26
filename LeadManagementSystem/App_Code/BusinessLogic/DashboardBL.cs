using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLogic;
using BusinessEntities;
using System.Data;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DashboardBL
/// </summary>
public class DashboardBL
{
	public DashboardBL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    DataUtilities dataUtilities = new DataUtilities();

    public DataSet GetLeadsCount()
    {
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetLeadsCount");
        return ds;
    }
    public DataSet GetLeadsCountByConsultant(int ConsultantID)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("@lsAssignedTo", ConsultantID);
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetLeadsCountByConsultant",hashtable);
        return ds;
    }
    public DataSet GetLeadsCountBySource(string Source)
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("@lsSourceRef", Source);
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetLeadsCountBySource", hashtable);
        return ds;
    }
    public DataSet GetLeadsCountQuoteToBooking()
    {
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetLeadsCountQuoteToBooking");
        return ds;
    }
    public DataSet GetOpenLeadsCount()
    {
        DataSet ds = dataUtilities.ExecuteDataSet("usp_GetOpenLeadsCount");
        return ds;
    }
}