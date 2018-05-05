using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
public partial class ProfilePage : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    consultantEntity consultant = new consultantEntity();
    ConsultantBL consultantBL = new ConsultantBL();
    EncryptDecrypt encrypydecrypt = new EncryptDecrypt();
    CommanClass _objComman = new CommanClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtPassword.Attributes["type"] = "password";
            GetStatus();
            GetDesignation();
            GetBranch();
            GetConsultants();
        }
    }

    protected void GetConsultants()
    {
        try
        {
            
            txtPassword.Attributes.Add("onfocus", "this.type='text';");
            dataset = consultantBL.GetConsultants(Convert.ToInt32(Session["ConsultantID"].ToString()));
            ViewState["ConsultantID"] = dataset.Tables[0].Rows[0]["ConsultantID"].ToString();
            txtFirstName.Text = dataset.Tables[0].Rows[0]["FirstName"].ToString();
            txtLastName.Text = dataset.Tables[0].Rows[0]["LastName"].ToString();
            txtMobile.Text = dataset.Tables[0].Rows[0]["Mobile"].ToString();
            txtEmail.Text = dataset.Tables[0].Rows[0]["Email"].ToString();
            txtLoginId.Text = dataset.Tables[0].Rows[0]["LoginID"].ToString();
            txtPassword.Text = encrypydecrypt.Decrypt(dataset.Tables[0].Rows[0]["Password"].ToString());
            ddlBranch.SelectedValue = dataset.Tables[0].Rows[0]["Branch"].ToString();
            ddlDesignation.SelectedValue = dataset.Tables[0].Rows[0]["Designation"].ToString();
            ddlStatus.SelectedValue = dataset.Tables[0].Rows[0]["Status"].ToString();          
        }
        catch
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
        catch
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
        catch
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
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            consultant.UpdatedBy = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = txtMobile.Text;
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = encrypydecrypt.Encrypt(txtPassword.Text.Trim());
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
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {

    }
}