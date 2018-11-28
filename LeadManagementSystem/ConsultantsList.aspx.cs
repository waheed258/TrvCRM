using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;

public partial class ConsultantsList : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    consultantEntity consultant = new consultantEntity();
    ConsultantBL consultantBL = new ConsultantBL();
    EncryptDecrypt encrypydecrypt = new EncryptDecrypt();
    CommanClass _objComman = new CommanClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                editConsultant.Visible = false;
                consultantslist.Visible = true;
                GetConsultants();
                GridView1.UseAccessibleHeader = true;
                GetStatus();
                GetDesignation();
                GetBranch();
            }
        }
        catch { }
    }
    protected void GetDesignation()
    {
        try
        {

            dataset = consultantBL.GetDesignation();
            ddlDesignation.DataSource = dataset;
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select Designation --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetBranch()
    {
        try
        {
            dataset = consultantBL.GetBranch();
            ddlBranch.DataSource = dataset;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--Select Branch --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetStatus()
    {
        try
        {
            dataset = consultantBL.GetStatus();
            ddlStatus.DataSource = dataset;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("--Select Status --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetConsultants()
    {
        try
        {
            dataset = consultantBL.GetConsultants(0);            
            GridView1.DataSource = dataset;
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                ViewState["ConsultantID"] = ((Label)row.FindControl("lblConsultantID")).Text.ToString();
                if (e.CommandName == "EditConsultant")
                {
                    editConsultant.Visible = true;
                    consultantslist.Visible = false;
                    dvRPwd.Visible = false;
                    txtPassword.Attributes.Add("onfocus", "this.type='text';");
                    txtFirstName.Text = ((Label)row.FindControl("lblFirstName")).Text.ToString();
                    txtLastName.Text = ((Label)row.FindControl("lblLastName")).Text.ToString();
                    txtEmail.Text = ((Label)row.FindControl("lblEmailID")).Text.ToString();
                    txtLoginId.Text = ((Label)row.FindControl("lblLoginID")).Text.ToString();
                    txtPassword.Text = encrypydecrypt.Decrypt(((Label)row.FindControl("lblPwd")).Text.ToString());
                    ddlBranch.SelectedValue = ((Label)row.FindControl("lblBranch")).Text.ToString();
                    ddlDesignation.SelectedValue = ((Label)row.FindControl("lblDesignation")).Text.ToString();
                    ddlStatus.SelectedValue = ((Label)row.FindControl("lblAStatus")).Text.ToString();
                }
                else if (e.CommandName == "DeleteConsultant")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
            }
        }
        catch
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            consultant.UpdatedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = "";
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = encrypydecrypt.Encrypt(txtPassword.Text.Trim());
            consultant.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
            consultant.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            consultant.Status = Convert.ToInt32(ddlStatus.SelectedValue);

            int result = consultantBL.CUDConsultant(consultant, 'U');
            if (result == 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Consultant details updated Successfully!";
                GetConsultants();
                dvRPwd.Visible = true;
                editConsultant.Visible = false;
                consultantslist.Visible = true;
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please try again!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        editConsultant.Visible = false;
        consultantslist.Visible = true;
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            consultant.UpdatedBy = 0;
            consultant.FirstName = "";
            consultant.LastName = "";
            consultant.Mobile = "";
            consultant.Email = "";
            consultant.LoginID = "";
            consultant.Password = "";
            consultant.Designation = 0;
            consultant.Branch = 0;
            consultant.Status = 0;

            int result = consultantBL.CUDConsultant(consultant, 'D');
            if (result == 1)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Consultant details deleted Successfully!";
                GetConsultants();
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please try again!";
            }
        }
        catch
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
}