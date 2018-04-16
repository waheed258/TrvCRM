using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class ConsultantList : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    consultantEntity consultant = new consultantEntity();
    ConsultantBL consultantBL = new ConsultantBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetConsultants();
            }
        }
        catch {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void GetConsultants()
    {
        try
        {
            dataset = consultantBL.GetConsultants(0);
            GridView1.DataSource = dataset;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
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
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int RowIndex = row.RowIndex;
            ViewState["ConsultantID"] = ((Label)row.FindControl("lblConsultantID")).Text.ToString();
            if (e.CommandName == "EditConsultant")
            {
                GetStatus();
                GetDesignation();
                GetBranch();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditModal();", true);
                txtFirstName.Text = ((Label)row.FindControl("lblFirstName")).Text.ToString();
                txtLastName.Text = ((Label)row.FindControl("lblLastName")).Text.ToString();
                txtMobile.Text = ((Label)row.FindControl("lblMobile")).Text.ToString();
                txtEmail.Text = ((Label)row.FindControl("lblEmailID")).Text.ToString();
                txtLoginId.Text = ((Label)row.FindControl("lblLoginID")).Text.ToString();
                txtPassword.Text = ((Label)row.FindControl("lblPwd")).Text.ToString();
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
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            //need to change updated by after session created
            consultant.UpdatedBy = 0;
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = txtMobile.Text;
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = txtPassword.Text.Trim();
            consultant.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
            consultant.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            consultant.Status = Convert.ToInt32(ddlStatus.SelectedValue);

            int result = consultantBL.CUDConsultant(consultant, 'U');
            if (result == 1)
            {
                message.ForeColor = System.Drawing.Color.Green;
                message.Text = "Consultant details updated Successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetConsultants();
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Text = "Please try again!";
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
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            //need to change updated by after session created
            consultant.UpdatedBy = 0;
            consultant.FirstName = "";
            consultant.LastName = "";
            consultant.Mobile = "";
            consultant.Email = "";
            consultant.LoginID ="";
            consultant.Password = "";
            consultant.Designation = 0;
            consultant.Branch = 0;
            consultant.Status = 0;

            int result = consultantBL.CUDConsultant(consultant, 'D');
            if (result == 1)
            {
                message.ForeColor = System.Drawing.Color.Green;
                message.Text = "Consultant details deleted Successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetConsultants();
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Text = "Please try again!";
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
}