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
public partial class UnAssignedLeads : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            GetLeadsList();
            gvLeadList.UseAccessibleHeader = true;
            gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void GetLeadsList()
    {
        try
        {
            dataset = leadBL.GetLeadsList(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvLeadList.DataSource = dataset;
                gvLeadList.DataBind();
                gvLeadList.UseAccessibleHeader = true;
            }


        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void gvLeadList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["LoginID"].ToString() == "admin")
            {
                ImageButton deleteButton = e.Row.FindControl("btnDelete") as ImageButton;
                deleteButton.Visible = true;
            }
            e.Row.BackColor = ((Label)e.Row.FindControl("lblDuplicateLeadList")).Text.ToString() == "Y" ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
            e.Row.ForeColor = ((Label)e.Row.FindControl("lblDuplicateLeadList")).Text.ToString() == "Y" ? System.Drawing.Color.White : System.Drawing.Color.Black;
        }
    }
    protected void gvLeadList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                Session["lsID"] = ((Label)row.FindControl("lblID")).Text.ToString();
                if (gvLeadList.Rows.Count > 0)
                {
                    gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (e.CommandName == "EditLead")
                {
                    Response.Redirect("EditLead.aspx", true);
                }
                else if (e.CommandName == "DeleteLead")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
                else if (e.CommandName == "Action")
                {
                    Response.Redirect("LeadAction.aspx?type=1", true);
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
                GetLeadsList();
                if (gvLeadList.Rows.Count > 0)
                {
                    gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
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