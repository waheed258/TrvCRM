using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LeadEntity
/// </summary>
public class LeadEntity
{
	public LeadEntity()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int LeadID { get; set; }
    public int SourceID { get; set; }
    public string SourceRef { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public int ProductType { get; set; }
    public string OriginName { get; set; }
    public string DestinationName { get; set; }
    public string DepartureDate { get; set; }
    public string ReturnDate { get; set; }
    public int Adult { get; set; }
    public int Child { get; set; }
    public int Infant { get; set; }
    public decimal Budget { get; set; }
    public string Notes { get; set; }
    public decimal QuotedPrice { get; set; }
    public decimal FinalPrice { get; set; }
    public int? CreatedBy { get; set; }
    public int Status { get; set; }
}