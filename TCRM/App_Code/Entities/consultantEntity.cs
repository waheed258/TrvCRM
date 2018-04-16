using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for consultantEntity
/// </summary>
public class consultantEntity
{

    public int ConsultantID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }    
    public string Email { get; set; }
    public string LoginID { get; set; }
    public string Password { get; set; }
    public int Designation { get; set; }
    public int Branch { get; set; }   
    public int Status { get; set; }
    public string Image { get; set; }   
    public int? UpdatedBy { get; set; }

}