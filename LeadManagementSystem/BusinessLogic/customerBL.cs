using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using DataLogic;
using BusinessEntities;
using System.Data;
namespace BusinessLogic
{
    /// <summary>
    /// Summary description for customerBL
    /// </summary>
    public class customerBL
    {
        DataUtilities dataUtilities = new DataUtilities();
        public customerBL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataSet GetCustomerList(int ID)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("@TravellerId", ID);
            DataSet ds = dataUtilities.ExecuteDataSet("usp_GetAllCustomers", hashtable);
            return ds;
        }


        public int CUDCustomer(customerEntity customerEntity, char Operation)
        {
            Hashtable hashtable = new Hashtable();
            if (Operation == 'I')
            {
                hashtable.Add("@TravellerId", 0);
                //need to change updated by after session createds
                hashtable.Add("@UpdatedBy", 0);
                //need to change updated by after session createds
                hashtable.Add("@CreatedBy", 0);

            }
            else
            {
                hashtable.Add("@TravellerId", customerEntity.TravellerId);
                //need to change updated by after session createds
                hashtable.Add("@UpdatedBy", 0);
                hashtable.Add("@CreatedBy", 0);
            }
            hashtable.Add("@TravellerTitel", customerEntity.TravellerTitel);
            hashtable.Add("@TravellerFirstName", customerEntity.TravellerFirstName);
            hashtable.Add("@TravellerLastName", customerEntity.TravellerLastName);
            hashtable.Add("@TravellerMailId", customerEntity.TravellerMailId);
            hashtable.Add("@TravellerPhone", customerEntity.TravellerPhone);
            hashtable.Add("@TravellerMobile", customerEntity.TravellerMobile);
            hashtable.Add("@TravellerAddress", customerEntity.TravellerAddress);
            hashtable.Add("@TravellerPassPortNo", customerEntity.TravellerPassPortNo);
            if (customerEntity.PassportIssueDate != "")
            {
                hashtable.Add("@PassportIssueDate", DateTime.ParseExact(customerEntity.PassportIssueDate, "dd-MM-yyyy", null));
            }
            else
            {
                hashtable.Add("@PassportIssueDate", DBNull.Value);
            }
            if (customerEntity.PassportExpiryDate != "")
            {
                hashtable.Add("@PassportExpiryDate", DateTime.ParseExact(customerEntity.PassportExpiryDate, "dd-MM-yyyy", null));
            }
            else
            {
                hashtable.Add("@PassportExpiryDate", DBNull.Value);
            }
            hashtable.Add("@CompanyId", customerEntity.CompanyId);
            hashtable.Add("@Operation", Operation);

            int result = dataUtilities.ExecuteNonQuery("usp_CUDCustomer", hashtable);
            return result;
        }
    }
}