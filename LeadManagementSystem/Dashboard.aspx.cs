using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
public partial class Dashboard : System.Web.UI.Page
{
    DashboardBL dashboardBL = new DashboardBL();
    ConsultantBL consultantBL = new ConsultantBL();
    LeadBL leadBL = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetConsultants();
            GetLeadsCount();
            if (Session["ConsultantID"].ToString() == "1")
            {
                GetLeadsCountByConsultant(0);
                ddlConsultantsAction.SelectedValue = "0";
            }
            else
            {
                GetLeadsCountByConsultant(Convert.ToInt32(Session["ConsultantID"].ToString()));
                ddlConsultantsAction.SelectedValue = Session["ConsultantID"].ToString();
            }

            GetLeadsCountQuoteToBooking();
            GetSourceData();
            GetLeadsCountBySource("0");
            GetOpenLeadsCount();
        }
    }
    private void GetLeadsCount()
    {
        DataSet dsLeadCount = dashboardBL.GetLeadsCount();
        today.InnerText = dsLeadCount.Tables[0].Rows[0]["TotalLeads"].ToString();
        thisweek.InnerText = dsLeadCount.Tables[1].Rows[0]["TotalLeads"].ToString();
        thismonth.InnerText = dsLeadCount.Tables[2].Rows[0]["TotalLeads"].ToString();
    }
    private void GetLeadsCountByConsultant(int ConsultantID)
    {
        DataSet dsLeadCountByConsultant = dashboardBL.GetLeadsCountByConsultant(ConsultantID);
        todayByConsultant.InnerText = dsLeadCountByConsultant.Tables[0].Rows[0]["TotalLeads"].ToString();
        thisweekByConsultant.InnerText = dsLeadCountByConsultant.Tables[1].Rows[0]["TotalLeads"].ToString();
        thismonthByConsultant.InnerText = dsLeadCountByConsultant.Tables[2].Rows[0]["TotalLeads"].ToString();
    }
    private void GetLeadsCountQuoteToBooking()
    {
        DataSet dsLeadCountQuoteToBooking = dashboardBL.GetLeadsCountQuoteToBooking();
        todayQB.InnerText = dsLeadCountQuoteToBooking.Tables[0].Rows[0]["TotalLeads"].ToString();
        thisweekQB.InnerText = dsLeadCountQuoteToBooking.Tables[1].Rows[0]["TotalLeads"].ToString();
        thismonthQB.InnerText = dsLeadCountQuoteToBooking.Tables[2].Rows[0]["TotalLeads"].ToString();
    }
    private void GetOpenLeadsCount()
    {
        DataSet dsOpenLeadsCount = dashboardBL.GetOpenLeadsCount();
        todayOpen.InnerText = dsOpenLeadsCount.Tables[0].Rows[0]["TotalLeads"].ToString();
    }

    protected void GetSourceData()
    {
        try
        {
            DataSet dataset = leadBL.GetSourceData("U");
            ddlSource.DataSource = dataset;
            ddlSource.DataTextField = "SourceType";
            ddlSource.DataValueField = "SourceTypeID";
            ddlSource.DataBind();
            ddlSource.Items.Insert(0, new ListItem("All", "0"));
        }
        catch
        {

        }
    }
    private void GetLeadsCountBySource(string Source)
    {
        DataSet dsLeadCountBySource = dashboardBL.GetLeadsCountBySource(Source);
        todaySource.InnerText = dsLeadCountBySource.Tables[0].Rows[0]["TotalLeads"].ToString();
        thisweekSource.InnerText = dsLeadCountBySource.Tables[1].Rows[0]["TotalLeads"].ToString();
        thismonthSource.InnerText = dsLeadCountBySource.Tables[2].Rows[0]["TotalLeads"].ToString();
    }
    protected void GetConsultants()
    {
        try
        {
            DataSet dataset = consultantBL.GetConsultants(0);
            ddlConsultantsAction.DataSource = dataset;
            ddlConsultantsAction.DataTextField = "Name";
            ddlConsultantsAction.DataValueField = "ConsultantID";
            ddlConsultantsAction.DataBind();
            ddlConsultantsAction.Items.Insert(0, new ListItem("All", "0"));
        }
        catch
        {

        }
    }
    protected void ddlConsultantsAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeadsCountByConsultant(Convert.ToInt32(ddlConsultantsAction.SelectedValue));
    }
    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeadsCountBySource(ddlSource.SelectedItem.Text);
    }
}