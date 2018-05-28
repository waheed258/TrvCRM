using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessEntities;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
public partial class Login : System.Web.UI.Page
{
    ConsultantBL consultantBL = new ConsultantBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["LoginId"] = null;
                Session["Password"] = null;
            }
        }
        catch { }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = consultantBL.ValidateUser(txtUserName.Text);
            string passowrd = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LoginID"].ToString() == "admin")
                {
                    passowrd = ds.Tables[0].Rows[0]["Password"].ToString();
                }
                else
                {
                    passowrd = Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                }
                if (passowrd == txtPassword.Text)
                {
                    Session["Name"] = ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"].ToString();
                    Session["LoginID"] = ds.Tables[0].Rows[0]["LoginID"].ToString();
                    Session["ConsultantID"] = ds.Tables[0].Rows[0]["ConsultantID"].ToString();
                    Response.Redirect("Lead.aspx", false);
                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Password is incorrect!";
                }
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "User Name is incorrect!";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Please check credentials";
        }
    }
    private string Decrypt(string cipherText)
    {
        try
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

        }

        catch { }
        return cipherText;
    }
}