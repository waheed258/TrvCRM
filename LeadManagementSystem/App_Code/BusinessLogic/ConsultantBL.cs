using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using DataLogic;
using BusinessEntities;

namespace BusinessLogic
{
    /// <summary>
    /// Summary description for ConsultantBL
    /// </summary>
    public class ConsultantBL
    {
        DataUtilities dataUtilities = new DataUtilities();
        public int CUDConsultant(consultantEntity consultantEntity, char Operation)
        {
            Hashtable hashtable = new Hashtable();
            if (Operation == 'I')
            {
                hashtable.Add("@ConsultantID", 0);
                //need to change updated by after session createds
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

            int result = dataUtilities.ExecuteNonQuery("usp_CUDConsultant", hashtable);
            return result;
        }
        public DataSet GetDesignation()
        {
            DataSet ds = dataUtilities.ExecuteDataSet("GetDesignation");
            return ds;
        }
        public DataSet GetBranch()
        {
            DataSet ds = dataUtilities.ExecuteDataSet("GetBranch");
            return ds;
        }

        public DataSet GetStatus()
        {
            DataSet ds = dataUtilities.ExecuteDataSet("GetStatus");
            return ds;
        }
        public DataSet GetConsultants(int ConsultantID)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@ConsultantID", ConsultantID);
            DataSet ds = dataUtilities.ExecuteDataSet("usp_GetAllConsultants", hashtable);
            return ds;
        }
        public DataSet ValidateUser(string LoginId)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@LoginId", LoginId);
            DataSet ds = dataUtilities.ExecuteDataSet("ValidateUser", hashtable);
            return ds;
        }
    }
}