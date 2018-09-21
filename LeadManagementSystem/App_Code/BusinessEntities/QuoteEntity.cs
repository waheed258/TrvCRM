using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QuoteEntity
/// </summary>
[Serializable()] 
public class QuoteEntity
{
    public string QuoteDate { get; set; }
    public string ToCity { get; set; }
    public string FlightDetails { get; set; }
    public string CarHireDetails { get; set; }
    public string HotelInfo { get; set; }
    public string ItineraryDetails { get; set; }
    public string Includes { get; set; }
    public string Excludes { get; set; }
    public int CostForAdultType { get; set; }
    public string CostForAdult { get; set; }
    public int NoOfAdults { get; set; }
    public int CostForChildType { get; set; }
    public string CostForChild { get; set; }
    public int NoOfChildren { get; set; }
    public string TravelInsurance { get; set; }
    public string ConsultantName { get; set; }
    public string AdultTotal { get; set; }
    public string ChildTotal { get; set; }

    public int LeadID { get; set; }
    public string IsMailSent { get; set; }
    public string QuoteNumber { get; set; }
    public string Operation { get; set; }
    public string PackageId { get; set; }

    public string TemplateName { get; set; }
    public string IsCustomTemplate { get; set; }

}