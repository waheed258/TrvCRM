using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

public partial class AssignedLeads : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();

    string ClientName = string.Empty;
    string product = string.Empty;
    string source = string.Empty;
    string toCity = string.Empty;
    string Email = string.Empty;
    string encryptedparamleadid = string.Empty;
    string encryptedparamlblProductID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetConsultants();
            if (Session["ConsultantID"].ToString() == "1")
            {
                ddlConsultantsFilter.SelectedValue = "-1";
            }
            else
            {
                ddlConsultantsFilter.SelectedValue = Session["ConsultantID"].ToString();
            }
            GetAssinedLeadsList();
            gvAssignedList.UseAccessibleHeader = true;
        }
    }
    protected void GetAssinedLeadsList()
    {
        try
        {
            if (ddlConsultantsFilter.SelectedValue == "-1")
            {
                dataset = leadBL.GetAssignedLeadsList(0);
            }
            else
            {
                dataset = leadBL.GetAssignedLeadsList(Convert.ToInt32(Session["ConsultantID"].ToString()));
            }
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvAssignedList.DataSource = dataset;
                gvAssignedList.DataBind();
                gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void ddlConsultantsFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlConsultantsFilter.SelectedValue == "-1")
            {
                dataset = leadBL.GetAssignedLeadsList(0);
            }
            else
            {
                dataset = leadBL.GetAssignedLeadsList(Convert.ToInt32(ddlConsultantsFilter.SelectedValue));
            }
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvAssignedList.DataSource = dataset;
                gvAssignedList.DataBind();
                gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch
        {

        }
    }

    private void GetConsultants()
    {
        try
        {
            DataSet dsConsultants = new DataSet();
            dsConsultants = consultantBL.GetConsultants(0);
            ddlConsultantsFilter.DataSource = dsConsultants;
            ddlConsultantsFilter.DataTextField = "Name";
            ddlConsultantsFilter.DataValueField = "ConsultantID";
            ddlConsultantsFilter.DataBind();
            ddlConsultantsFilter.Items.Insert(0, new ListItem("All", "-1"));
        }
        catch { }
    }
    protected void gvAssignedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["LoginID"].ToString() == "admin")
            {
                ImageButton deleteButton = e.Row.FindControl("btnDelete") as ImageButton;
                //if (deleteButton == null) { return; }
                //else
                deleteButton.Visible = true;
            }
            e.Row.BackColor = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
            e.Row.ForeColor = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? System.Drawing.Color.White : System.Drawing.Color.Black;
        }

    }
    protected void gvAssignedList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                if (gvAssignedList.Rows.Count > 0)
                {
                    gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                Session["lsID"] = ((Label)row.FindControl("lblID")).Text.ToString();
                if (e.CommandName == "EditLead")
                {
                    ViewState["lsLeadActionsID"] = ((Label)row.FindControl("lsLeadActionsID")).Text.ToString();
                    ClientName = ((Label)row.FindControl("lblFirstName")).Text.ToString() + " " + ((Label)row.FindControl("lblLastName")).Text.ToString();
                    product = ((Label)row.FindControl("lblProdType")).Text.ToString();
                    source = (((Label)row.FindControl("lblOrigin")).Text.ToString());
                    toCity = ((Label)row.FindControl("lblDestination")).Text.ToString();
                    Email = ((Label)row.FindControl("lblEmailID")).Text.ToString();
                    encryptedparamleadid = Session["lsID"].ToString();
                    encryptedparamlblProductID = ((Label)row.FindControl("lblProductID")).Text.ToString();
                    Response.Redirect("EditLead.aspx?ClientName=" + ClientName + "&product=" + product + "&source=" + source + "&toCity=" + toCity + "&Email=" + Email + "&encryptedparamleadid=" + encryptedparamleadid + "&encryptedparamlblProductID=" + encryptedparamlblProductID + "&LeadActionID=" + ViewState["lsLeadActionsID"].ToString());
                }
                else if (e.CommandName == "DeleteLead")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
                else if (e.CommandName == "Action")
                {
                    Response.Redirect("LeadAction.aspx?type=2", true);
                }
            }
        }
        catch
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            leadEntity.LeadID = Convert.ToInt32(Session["lsID"].ToString());
            leadEntity.SourceID = 0;
            leadEntity.SourceRef = "";
            leadEntity.Others = "";
            leadEntity.AssignedTo = 0;
            leadEntity.AssignedBy = 0;
            leadEntity.FirstName = "";
            leadEntity.LastName = "";
            leadEntity.Mobile = "";
            leadEntity.Email = "";
            leadEntity.OriginName = "";
            leadEntity.DestinationName = "";
            leadEntity.DepartureDate = "";
            leadEntity.ReturnDate = "";
            leadEntity.Adult = 0;
            leadEntity.Child = 0;
            leadEntity.Infant = 0;
            leadEntity.ProductType = 0;
            leadEntity.Budget = 0;
            leadEntity.Notes = "";
            leadEntity.QuotedPrice = 0;
            leadEntity.FinalPrice = 0;
            leadEntity.UpdatedBy = 0;
            leadEntity.LeadStatus = 0;
            leadEntity.CreatedBy = 0;
            leadEntity.PackageId = "";
            leadEntity.FollowupDate = "";
            leadEntity.FollowupDesc = "";
            leadEntity.LeadDescription = "";
            leadEntity.ProductID = "";
            leadEntity.WebsiteConsultantNotes = "";
            int result = leadBL.CUDLead(leadEntity, 'D');
            if (result == 0)
            {
                lblMessage.Text = "Lead Details deleted Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                GetAssinedLeadsList();
                if (gvAssignedList.Rows.Count > 0)
                {
                    gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            else
            {
                lblMessage.Text = "Please try again!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
}