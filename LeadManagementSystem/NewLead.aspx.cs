using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
public partial class NewLead : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    int j = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _objComman.getRecordsPerPage(DropPage);
                GetProducts();
                GetSourceData();
                others.Visible = false;
                GetLeadsList();
                newlead.Visible = false;
                btnUpdate.Visible = false;
            }
        }
        catch { }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeadsList();
    }
    protected void GetLeadsList()
    {
        try
        {
            gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = leadBL.GetLeadsList(0);
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
    protected void GetSourceData()
    {
        try
        {
            dataset = leadBL.GetSourceData();
            ddlSource.DataSource = dataset;
            ddlSource.DataTextField = "SourceType";
            ddlSource.DataValueField = "SourceTypeID";
            ddlSource.DataBind();
            ddlSource.Items.Insert(0, new ListItem("--Select Source --", "-1"));
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GetProducts()
    {
        try
        {
            dataset = leadBL.GetProduct();
            ddlPackage.DataSource = dataset;
            ddlPackage.DataTextField = "ProductType";
            ddlPackage.DataValueField = "ProductTypeID";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("--Select Product --", "-1"));
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                ViewState["lsID"] = ((Label)row.FindControl("lblID")).Text.ToString();
                if (e.CommandName == "EditLead")
                {
                    newlead.Visible = true;
                    LeadList.Visible = false;
                    imgbtnAddLead.Visible = false;
                    btnUpdate.Visible = true;
                    ImageButton1.Visible = false;
                    GetProducts();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditModal();", true);
                    ddlSource.SelectedValue = ((Label)row.FindControl("lbllsSource")).Text.ToString();
                    if (ddlSource.SelectedValue == "10")
                    {
                        others.Visible = true;
                    }
                    txtOthers.Text = ((Label)row.FindControl("lbllsOthersInfo")).Text.ToString();
                    txtFirstName.Text = ((Label)row.FindControl("lblFirstName")).Text.ToString();
                    txtLastName.Text = ((Label)row.FindControl("lblLastName")).Text.ToString();
                    txtMobile.Text = ((Label)row.FindControl("lblMobile")).Text.ToString();
                    txtEmail.Text = ((Label)row.FindControl("lblEmailID")).Text.ToString();
                    txtSource.Text = ((Label)row.FindControl("lblOrigin")).Text.ToString();
                    txtDestination.Text = ((Label)row.FindControl("lblDestination")).Text.ToString();
                    ddlPackage.SelectedValue = ((Label)row.FindControl("lblProdID")).Text.ToString();
                    ddlAdults.SelectedValue = ((Label)row.FindControl("lblAdult")).Text.ToString();
                    ddlChild.SelectedValue = ((Label)row.FindControl("lblChildren")).Text.ToString();
                    ddlInfant.SelectedValue = ((Label)row.FindControl("lblInfants")).Text.ToString();
                    txtDepart.Text = ((Label)row.FindControl("lblDepartDate")).Text.ToString();
                    txtReturnDate.Text = ((Label)row.FindControl("lblReturnDate")).Text.ToString();
                    txtBudget.Text = ((Label)row.FindControl("lblBudget")).Text.ToString();
                    txtNotes.Text = ((Label)row.FindControl("lblNotes")).Text.ToString();
                }
                else if (e.CommandName == "DeleteLead")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
            }
        }
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
            leadEntity.SourceID = 0;
            leadEntity.SourceRef = "";
            leadEntity.Others = txtOthers.Text;
            leadEntity.FirstName = txtFirstName.Text;
            leadEntity.LastName = txtLastName.Text;
            leadEntity.Mobile = txtMobile.Text;
            leadEntity.Email = txtEmail.Text;
            leadEntity.OriginName = txtSource.Text;
            leadEntity.DestinationName = txtDestination.Text;
            leadEntity.DepartureDate = txtDepart.Text;
            leadEntity.ReturnDate = txtReturnDate.Text;
            leadEntity.Adult = 0;
            leadEntity.Child = 0;
            leadEntity.Infant = 0;
            leadEntity.ProductType = 0;
            leadEntity.Budget = 0;
            leadEntity.Notes = txtNotes.Text;
            leadEntity.QuotedPrice = 0;
            leadEntity.FinalPrice = 0;

            int result = leadBL.CUDLead(leadEntity, 'D');
            if (result == 1)
            {
                message.Text = "Lead Details deleted Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetLeadsList();
                Clear();

            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch (Exception ex)
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }


    protected void ddlSource_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "10")
        {
            txtOthers.Text = "";
            others.Visible = true;
        }
        else
        {
            txtOthers.Text = "";
            others.Visible = false;
        }
    }
    private void Clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtSource.Text = "";
        txtDestination.Text = "";
        txtDepart.Text = "";
        txtReturnDate.Text = "";
        ddlAdults.SelectedValue = "1";
        ddlChild.SelectedValue = "0";
        ddlInfant.SelectedValue = "0";
        ddlPackage.SelectedValue = "-1";
        txtBudget.Text = "";
        txtNotes.Text = "";
    }
    protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeadList.PageIndex = e.NewPageIndex;
        GetLeadsList();
    }
    protected void imgbtnAddLead_Click(object sender, ImageClickEventArgs e)
    {
        newlead.Visible = true;
        LeadList.Visible = false;
        imgbtnAddLead.Visible = false;
        btnUpdate.Visible = false;
        ImageButton1.Visible = true;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        newlead.Visible = false;
        LeadList.Visible = true;
        imgbtnAddLead.Visible = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int a = Convert.ToInt32(ddlAdults.SelectedValue);
            int c = Convert.ToInt32(ddlChild.SelectedValue);
            int i = Convert.ToInt32(ddlInfant.SelectedValue);
            j = a + c + i;
            if (i <= a)
            {
                if (a == 9)
                {
                    if (ddlChild.SelectedIndex != 0 || ddlInfant.SelectedIndex != 0)
                        message.ForeColor = System.Drawing.Color.Red;
                    message.Text = "No of Pax should not exceed 9";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (j > 9)
                    {
                        message.Text = "No of Pax should not exceed 9";
                        message.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
                        leadEntity.SourceID = Convert.ToInt32(ddlSource.SelectedValue);
                        leadEntity.SourceRef = ddlSource.SelectedItem.Text;
                        leadEntity.Others = txtOthers.Text;
                        leadEntity.FirstName = txtFirstName.Text;
                        leadEntity.LastName = txtLastName.Text;
                        leadEntity.Mobile = txtMobile.Text;
                        leadEntity.Email = txtEmail.Text;
                        leadEntity.OriginName = txtSource.Text;
                        leadEntity.DestinationName = txtDestination.Text;
                        leadEntity.DepartureDate = txtDepart.Text;
                        leadEntity.ReturnDate = txtReturnDate.Text;
                        leadEntity.Adult = a;
                        leadEntity.Child = c;
                        leadEntity.Infant = i;
                        leadEntity.ProductType = Convert.ToInt32(ddlPackage.SelectedValue);
                        leadEntity.Budget = Convert.ToDecimal(txtBudget.Text);
                        leadEntity.Notes = txtNotes.Text;
                        leadEntity.QuotedPrice = 0;
                        leadEntity.FinalPrice = 0;
                        int result = leadBL.CUDLead(leadEntity, 'U');
                        if (result == 1)
                        {
                            message.Text = "Lead Details updated Successfully!";
                            message.ForeColor = System.Drawing.Color.Green;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            LeadList.Visible = true;
                            newlead.Visible = false;
                            GetLeadsList();
                            Clear();

                        }
                        else
                        {
                            message.Text = "Please try again!";
                            message.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
            }
            else
            {
                message.Text = "No of infants should not exceed adults";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int a = Convert.ToInt32(ddlAdults.SelectedValue);
            int c = Convert.ToInt32(ddlChild.SelectedValue);
            int i = Convert.ToInt32(ddlInfant.SelectedValue);
            j = a + c + i;
            if (i <= a)
            {
                if (a == 9)
                {
                    if (ddlChild.SelectedIndex != 0 || ddlInfant.SelectedIndex != 0)
                        message.ForeColor = System.Drawing.Color.Red;
                    message.Text = "No of Pax should not exceed 9";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (j > 9)
                    {
                        message.Text = "No of Pax should not exceed 9";
                        message.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        leadEntity.SourceID = Convert.ToInt32(ddlSource.SelectedValue);
                        leadEntity.SourceRef = ddlSource.SelectedItem.Text;
                        leadEntity.Others = txtOthers.Text;
                        leadEntity.FirstName = txtFirstName.Text;
                        leadEntity.LastName = txtLastName.Text;
                        leadEntity.Mobile = txtMobile.Text;
                        leadEntity.Email = txtEmail.Text;
                        leadEntity.OriginName = txtSource.Text;
                        leadEntity.DestinationName = txtDestination.Text;
                        leadEntity.DepartureDate = txtDepart.Text;
                        leadEntity.ReturnDate = txtReturnDate.Text;
                        leadEntity.Adult = a;
                        leadEntity.Child = c;
                        leadEntity.Infant = i;
                        leadEntity.ProductType = Convert.ToInt32(ddlPackage.SelectedValue);
                        leadEntity.Budget = Convert.ToDecimal(txtBudget.Text);
                        leadEntity.Notes = txtNotes.Text;
                        leadEntity.QuotedPrice = 0;
                        leadEntity.FinalPrice = 0;
                        int result = leadBL.CUDLead(leadEntity, 'I');
                        if (result == 1)
                        {
                            message.Text = "Lead Details saved Successfully!";
                            message.ForeColor = System.Drawing.Color.Green;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            Clear();
                            LeadList.Visible = true;
                            newlead.Visible = false;
                            imgbtnAddLead.Visible = true;

                        }
                        else
                        {
                            message.Text = "Please try again!";
                            message.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
            }
            else
            {
                message.Text = "No of infants should not exceed adults";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}