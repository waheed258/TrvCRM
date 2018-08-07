<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="QuoteTemplate.aspx.cs" Inherits="QuoteTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="wrapper" style="width: 210mm; height: 275mm; background-color: #fff; margin: 0 auto;">
        <div style="width: 100%; display: inline-block; margin-bottom: 2mm; clear: both;">
            <img style="max-width: 100%; display: block;" src="images/ticket_head.jpg" alt="ticket-head" />
        </div>
        <div class="body_cnt" style="width: 185mm; margin: auto;">
            <table style="width: 100%; text-align: center; margin: 0 0 2mm;">
                <tr>
                    <td>
                        <h3 style="font-size: 5mm; color: #25377b; margin: 0 0 1mm; font-weight: 800; text-transform: uppercase;">Travel Quotation</h3>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #b9b9b9; border-spacing: 0; margin: 0 0 3mm;">
                <tr>
                    <td width="30%" style="padding: 5px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%; text-transform: uppercase;">Quotation For</h5>
                    </td>
                    <td width="70%" style="padding: 5px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <%--<p style="font-size: 2.5mm; margin: 0; width: 50%;">Kovalain Naidu</p>--%>
                        <asp:Label ID="lblQuoteFor" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="30%" style="padding: 5px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%; text-transform: uppercase;">Date</h5>
                    </td>
                    <td width="70%" style="padding: 5px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <asp:Label ID="lblDate" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td width="30%" style="padding: 5px 10px 5px; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%; text-transform: uppercase;">Booking Ref</h5>
                    </td>
                    <td width="70%" style="padding: 5px 10px 5px;">
                        <asp:Label ID="lblBookingRefNum" runat="server"></asp:Label>

                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #b9b9b9; border-spacing: 0; margin: 0 0 3mm;">
                <tr>
                    <td colspan="4" width="100%" style="font-weight: 700; background-color: #00aeef; width: 100%; padding: 5px 10px; color: #fff; font-size: 3.56mm; text-transform: uppercase;">Flight Quotation </td>
                </tr>
                <tr>
                    <td width="20%" style="font-weight: 700; background-color: #e0f5fd; width: 20%; padding: 5px 10px; border-right: 1px solid #b9b9b9; color: #25377b; font-size: 3.2mm; text-transform: uppercase;">Flight No </td>
                    <td width="20%" style="font-weight: 700; background-color: #e0f5fd; width: 20%; padding: 5px 10px; border-right: 1px solid #b9b9b9; color: #25377b; font-size: 3.2mm; text-transform: uppercase;">Date</td>
                    <td width="40%" style="font-weight: 700; background-color: #e0f5fd; width: 40%; padding: 5px 10px; border-right: 1px solid #b9b9b9; color: #25377b; font-size: 3.2mm; text-transform: uppercase;">Traveling to</td>
                    <td width="20%" style="font-weight: 700; background-color: #e0f5fd; width: 20%; padding: 5px 10px; color: #25377b; font-size: 3.2mm; text-transform: uppercase;">Fare</td>
                </tr>
                <tr>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">EK 776 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">24MAY </p>
                    </td>
                    <td width="40%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">DURBAN/DUBAI HK2   1840 0500 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">R 12561</p>
                    </td>
                </tr>
                <tr>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">EK 398 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">25MAY </p>
                    </td>
                    <td width="40%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">DUBAI BALI  	HK2    0910 2220 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">R 2546</p>
                    </td>
                </tr>
                <tr>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">EK 361 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">30MAY </p>
                    </td>
                    <td width="40%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">BALI DUBAI   HK2  1630 2135 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">R 12352</p>
                    </td>
                </tr>
                <tr>
                    <td width="20%" style="padding: 10px 10px 5px; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">EK 775 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">02june </p>
                    </td>
                    <td width="40%" style="padding: 10px 10px 5px; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">DUBAI DURBAN  HK2  0955 1620 </p>
                    </td>
                    <td width="20%" style="padding: 10px 10px 5px;">
                        <p style="font-size: 2.5mm; margin: 0;">R 54646</p>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #b9b9b9; border-spacing: 0; margin: 0 0 3mm;">
                <tr>
                    <td width="55%" style="font-weight: 700; background-color: #00aeef; padding: 5px 10px; color: #fff; font-size: 3.56mm; text-transform: uppercase;">Include</td>
                    <td width="45%" style="font-weight: 700; background-color: #00aeef; padding: 5px 10px; color: #fff; font-size: 3.56mm; text-transform: uppercase;">Exclude</td>
                </tr>
                <tr>
                    <td width="55%" style="padding: 0px 10px 0px; border-right: 1px solid #b9b9b9;">
                        <ul style="padding-left: 10px;">
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Return flights on Emirates and Bangkok Airways and related taxes</li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">7 Nights  Phuket Novotel Phuket deluxe family room  with breakfast </li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">5 Nights Dubai Gloria Hotel 1 x grand suite city view room only basis  </li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Return  airport/hotel/airport transfers</li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Dubai Visa </li>
                        </ul>
                    </td>
                    <td width="45%" style="padding: 0px 10px 0px;">
                        <ul style="padding-left: 10px;">
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Meals & beverages not specified</li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Early check in, late check-out. </li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Anything not mentioned under the inclusions </li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Items of a personal nature</li>
                            <li style="font-size: 2.5mm; margin: 0; padding: 2px 0px;">Travel & Medical Insurance </li>
                        </ul>
                    </td>
            </table>

            <table style="width: 100%; border: 1px solid #b9b9b9; border-spacing: 0; margin: 0 0 3mm;">

                <tr>
                    <td width="30%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%;">COST PER Adult</h5>
                    </td>
                    <td width="55%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0; width: 50%;">R 31540.00 X 2</p>
                    </td>
                    <td width="15%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0; width: 100%; font-weight: bold;">R 93000.00</p>
                    </td>
                </tr>
                <tr>
                    <td width="30%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%;">COST PER Child
                        </h5>
                    </td>
                    <td width="55%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0; width: 50%;">R 15630.00 x 1</p>
                    </td>
                    <td width="15%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0; width: 100%; font-weight: bold;">R 15630.00</p>
                    </td>
                </tr>
                <tr>
                    <td width="30%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%;">LESS PAYMENT RECEIVED</h5>
                    </td>
                    <td width="55%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 3.05mm; margin: 0; width: 50%;"></p>
                    </td>
                    <td width="15%" style="padding: 10px 10px 5px; border-bottom: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0; width: 100%; font-weight: bold;">R 55000.00</p>
                    </td>
                </tr>
                <tr>
                    <td width="30%" style="padding: 10px 10px 5px; border-right: 1px solid #b9b9b9;">
                        <h5 style="font-size: 3.05mm; margin: 0; color: #00aeef; width: 100%;">BALANCE DUE
                        </h5>
                    </td>
                    <td width="55%" style="padding: 10px 10px 5px; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 3.05mm; margin: 0; width: 50%;"></p>
                    </td>
                    <td width="15%" style="padding: 10px 10px 5px;">
                        <p style="font-size: 2.5mm; margin: 0; width: 100%; font-weight: bold;">R 40000.00</p>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; border: 1px solid #b9b9b9; border-spacing: 0; margin: 0 0 3mm;">
                <tr>
                    <td width="55%" style="font-weight: 700; background-color: #00aeef; padding: 5px 10px; color: #fff; font-size: 2.5mm; text-transform: uppercase;">Travel Insurance (highly recommended) </td>
                    <td width="45%" style="font-weight: 700; background-color: #00aeef; padding: 5px 10px; color: #fff; font-size: 2.5mm; text-transform: uppercase;">Visas</td>
                </tr>
                <tr>
                    <td width="55%" style="padding: 10px 10px 10px; border-right: 1px solid #b9b9b9;">
                        <p style="font-size: 2.5mm; margin: 0;">We recommend: Travel Policy @ R 350.00pp,</p>
                        <p style="font-size: 2.5mm; margin: 0;">
                            R12 million medical cover, max 30 days.
                        </p>
                    </td>
                    <td width="45%" style="padding: 10px 10px 10px;">
                        <p style="font-size: 2.5mm; margin: 0;">Kindly check all visa requirements</p>
                        <p style="font-size: 2.5mm; margin: 0;">https://www.svsvisaservices.co.za/</p>
                    </td>
                </tr>
            </table>
            <p style="font-size: 2.5mm; margin-bottom: 1mm;"><b style="font-size: 2.5mm; color: #00aeef;">TO BOOK: </b>Please send through names and surnames, as per passports in order to secure seats. Passports must be valid for min 6 months after return date to South Africa, with min. 4 blank pages</p>
            <p style="font-size: 2.5mm; margin-bottom: 1mm;"><b style="font-size: 2.5mm; color: #00aeef;">important update:  </b>Children 18 years and under travelling with families, or unaccompanied, must travel with an unabridged birth certificate and a valid passport, when travelling in or out of South Africa.</p>

            <p style="font-size: 2.5mm; color: #ff0000; margin-bottom: 1mm; width: 100%; font-weight: bold;">Due to the current volatility of the Rand (ZAR), please call us prior to making full payment on any bookings.</p>
            <b style="font-size: 2.5mm; color: #00aeef;">Kind Regards</b></br>
            <b style="font-size: 2.5mm; color: #00aeef;">(Consultant name)</b>
            <h6 style="font-size: 3mm; color: #00aeef; margin-bottom: 1mm; margin-top: 4mm; width: 100%;">BANKING DETAILS</h6>
            <p style="margin: 0 0 2mm;"><b>NEDBANK</b> Serendipity Tours, Branch code; 13 05 26 00Smith Street (Durban), Account: 1305 716 582; Swift code: NEDSZAJJ</p>
            <p style="margin: 0 0 2mm;"><b>FNB</b> Serendipity Tours cc, Branch code: 221 426– Durban; Account:  6209 525 1329</p>
            <p style="margin: 0 0 2mm;"><b>STANDARD BANK</b> Serendipity Tours cc; Branc Code: 042626– Musgrave Road (Durban); Account: 05 122 708 8</p>
            <p style="margin: 0 0 2mm;">PLEASE USE YOUR BOOKING REF AND FIRST NAME AS REFERENCE</p>
        </div>
        <div style="width: 100%; display: inline-block; margin-top: 3mm;">
            <img style="max-width: 100%; display: block;" src="images/ticket_foot.jpg" alt="ticket-head" />
        </div>
    </div>
</asp:Content>

