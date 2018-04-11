using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using DataManager;

/// <summary>
/// Summary description for ConsultantBL
/// </summary>
public class ConsultantBL
{
    DataUtilities dataUtilities = new DataUtilities();
    public int CUDConsultant(consultantEntity consultantEntity, char Operation)
    {
        Hashtable hashtable = new Hashtable();
        if (Operation == 'i')
        {
            hashtable.Add("@ConsultantID", 0);
            hashtable.Add("@UpdatedBy", 0);

        }
        else
        {
            hashtable.Add("@ConsultantID", consultantEntity.ConsultantID);
            hashtable.Add("@UpdatedBy", consultantEntity.UpdatedBy);
        }

        hashtable.Add("@FirstName", consultantEntity.FirstName);
        hashtable.Add("@LastName", consultantEntity.LastName);
        hashtable.Add("@Mobile", consultantEntity.Mobile);
        hashtable.Add("@EmailID", consultantEntity.Email);
        hashtable.Add("@LoginId", consultantEntity.LoginID);
        hashtable.Add("@Password", consultantEntity.Password);
        hashtable.Add("@Designation", consultantEntity.Designation);
        hashtable.Add("@Branch", consultantEntity.Branch);
        hashtable.Add("@Status", consultantEntity.Status);
        hashtable.Add("@Image", consultantEntity.Image);       
        hashtable.Add("@Operation", Operation);

        int result = dataUtilities.ExecuteNonQuery("AdvisorCRUD", hashtable);
        return result;
    }
}