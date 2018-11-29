using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewQuotePDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/ViewQuotePDF/") + Request.QueryString["FN"];
        this.Response.ContentType = "application/pdf";
        this.Response.AppendHeader("Content-Disposition;", "attachment;filename=" + Request.QueryString["FN"]);
        this.Response.WriteFile(filePath);
        this.Response.End();
    }
}