using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;

public partial class Layout : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["Name"] != null)
        {
            profile.InnerHtml = Session["Name"].ToString();
            if (Session["ConsultantID"].ToString() == "1")
            {
                profileli.Visible = false;
            }
            else
            {
                profileli.Visible = true;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}
