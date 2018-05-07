using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
public partial class Reports : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _objComman.getRecordsPerPage(DropPage);              
                GetLeadsList();    
            }
        }
        catch { }
    }
    protected void GetLeadsList()
    {
        try
        {
            gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = leadBL.GetLeadsList(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                search.Visible = true;
            }
            else
            {
                search.Visible = false;
            }
            gvLeadList.DataSource = dataset;
            gvLeadList.DataBind();
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeadsList();
    }
    protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeadList.PageIndex = e.NewPageIndex;
        GetLeadsList();
    }
    protected void gvLeadList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}