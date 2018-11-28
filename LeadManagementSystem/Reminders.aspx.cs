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

public partial class Reminders : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetReminders();
                gvReminders.UseAccessibleHeader = true;
            }
        }
        catch { }
    }
    protected void GetReminders()
    {
        try
        {
            dataset = leadBL.GetReminders();
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvReminders.DataSource = dataset;
                gvReminders.DataBind();
                gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}