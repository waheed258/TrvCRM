<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnquiryForm.aspx.cs" Inherits="EnquiryForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enquiry Form</title>
    <!-- Meta tags -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Enquiry Form" />
    <script type="application/x-javascript">
		addEventListener("load", function () {
			setTimeout(hideURLbar, 0);
		}, false);

		function hideURLbar() {
			window.scrollTo(0, 1);
		}
    </script>
    <!-- //Meta tags -->
    <!-- Stylesheet -->
    <link href="css/wickedpicker.css" rel="stylesheet" type='text/css' media="all" />
    <link rel="stylesheet" href="css/jquery-ui.css" />
    <link href="css/enqstyle.css" rel="stylesheet" />

    <!-- //Stylesheet -->
    <!--fonts-->
    <link href="//fonts.googleapis.com/css?family=Roboto:300,400,500,700" rel="stylesheet" />
    <link href="//fonts.googleapis.com/css?family=Raleway:300,400,500,600,700" rel="stylesheet" />
    <link href="//fonts.googleapis.com/css?family=Megrim" rel="stylesheet" />
    <!--//fonts-->

    <script src="js/jquery-2.2.3.min.js"></script>
    
    <script>
        $(document).ready(function () {
            $("#txtReturnDate").prop('disabled', true);
            $('#txtDepart').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#txtReturnDate").prop('disabled', false);
                    $("#txtReturnDate").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#txtReturnDate").datepicker("option", "minDate", date)
                }
            });
            $("#txtReturnDate").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });

            $("#txtMobile,#txtBudget").bind('keypress', function (e) {
                if (e.keyCode == '9' || e.keyCode == '16') {
                    return;
                }
                var code;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                if (e.which == 46)
                    return false;
                if (code == 8 || code == 46)
                    return true;
                if (code < 48 || code > 57)
                    return false;
            });
            $("#txtMobile,#txtBudget").bind('mouseenter', function (e) {
                var val = $(this).val();
                if (val != '0') {
                    //  val = val.replace(/[^0-9]+/g, "");
                    $(this).val(val);
                }
            });
            $('#txtBudget').on('change', function () {
                this.value = parseFloat(this.value).toFixed(2);
            });
           
        });
    </script>

</head>
<body>
   <form id="form1" runat="server">
        <h1>Enquiry Form </h1>
        <div class="booking-form-w3layouts">
             <div class="main-flex-w3ls-sectns">               
                 <asp:Label ID="message" runat="server" style="color:#d2741c;margin-left:25%;font-size:20px;"></asp:Label>
             </div>
            

            <h2 class="sub-heading-agileits">Booking Details</h2>

            <div class="main-flex-w3ls-sectns">
                <div class="field-agileinfo-spc form-w3-agile-text1">
                    <asp:TextBox ID="txtSource" class="form-control" runat="server" placeholder="From"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSource" runat="server" ControlToValidate="txtSource" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Source" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="field-agileinfo-spc form-w3-agile-text1">
                    <asp:TextBox ID="txtDestination" class="form-control" runat="server" placeholder="To"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ControlToValidate="txtDestination" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Destination" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="main-flex-w3ls-sectns">
                <div class="field-agileinfo-spc form-w3-agile-text1">
                    <asp:TextBox ID="txtDepart" class="form-control" runat="server" placeholder="Deapart (DD-MM-YYYY)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDepart" runat="server" ControlToValidate="txtDepart" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Depart Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="field-agileinfo-spc form-w3-agile-text2">
                    <asp:TextBox ID="txtReturnDate" class="form-control" runat="server" placeholder="Return (DD-MM-YYYY)"></asp:TextBox>
                </div>
            </div>

            <div class="triple-wthree">
                <div class="field-agileinfo-spc form-w3-agile-text11">

                    <asp:DropDownList ID="ddlAdults" class="form-control" runat="server">
                        <asp:ListItem Value="">Adult(12+ Yrs)</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="field-agileinfo-spc form-w3-agile-text22">
                    <asp:DropDownList ID="ddlChild" class="form-control" runat="server">
                        <asp:ListItem Value="">Children(2-11 Yrs)</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>


                </div>
                <div class="field-agileinfo-spc form-w3-agile-text33">
                    <asp:DropDownList ID="ddlInfant" class="form-control" runat="server">
                        <asp:ListItem Value="">Infant(under 2Yrs)</asp:ListItem>
                        <asp:ListItem Value="0">0</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="main-flex-w3ls-sectns">
                <div class="field-agileinfo-spc form-w3-agile-text1">
                    <asp:DropDownList ID="ddlPackage" class="form-control" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage" ForeColor="#d0582e"
                        ErrorMessage="Please Select Product" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="field-agileinfo-spc form-w3-agile-text2">
                    <asp:TextBox ID="txtBudget" runat="server" class="form-control" placeholder="Price"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBudget" runat="server" ControlToValidate="txtBudget" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Budget" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <h3 class="sub-heading-agileits">Personal Details</h3>

            <div class="main-flex-w3ls-sectns">
                <div class="field-agileinfo-spc form-w3-agile-text1">
                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="First Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                        ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="field-agileinfo-spc form-w3-agile-text2">
                    <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="main-flex-w3ls-sectns">
                <div class="field-agileinfo-spc form-w3-agile-text1">
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                    </asp:RegularExpressionValidator>
                </div>
                <div class="field-agileinfo-spc form-w3-agile-text2">
                    <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                        ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                </div>
            </div>

            <div class="field-agileinfo-spc form-w3-agile-text">
                <asp:TextBox ID="txtNotes" runat="server" class="form-control" placeholder="Any Message..." TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
            </div>

            <div class="clear"></div>
            <div></div>
            <div class="main-flex-w3ls-sectns">
                 <div class="field-agileinfo-spc form-w3-agile-button1">
                     <asp:Button ID="btnSubmit" runat="server" ValidationGroup="Consultant" Text="Submit" CssClass="submit" OnClick="btnSubmit_Click" />
                 </div>
                 <div class="field-agileinfo-spc form-w3-agile-button2">
                     <asp:Button ID="btnClear" runat="server"  Text="Clear From" CssClass="clear" OnClick="btnClear_Click" />
                 </div>
                
            </div>


            <div class="clear"></div>

            <style>
                .submit {                    
                    background-color: #d2741c;
                    margin-right:0em !important;
                }

                    .submit:hover {
                        background-color: #0091cd;
                    }

                .clear {                   
                    background-color: #0091cd !important;
                    margin-right: 20.5em !important;
                }

                .submit:hover {
                    background-color: #d2741c !important;
                }
            </style>

        </div>
        <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    </form>
</body>
</html>
