<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Lead.aspx.cs" Inherits="Lead" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="js/jquery-2.1.4.min.js"></script>
    <script src="ckeditor/ckeditor.js"></script>
    <style>
        .LeadColor {
            width: 50px;
            height: 30px;
            background-color: LightBlue;
            margin-right: 15px;
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtReturnDate").prop('disabled', true);
            $('#ContentPlaceHolder1_txtDepart').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtReturnDate").prop('disabled', false);
                    $("#ContentPlaceHolder1_txtReturnDate").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#ContentPlaceHolder1_txtReturnDate").datepicker("option", "minDate", date)
                }
            });
            $("#ContentPlaceHolder1_txtReturnDate").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });
            $("#ContentPlaceHolder1_txtFollowUp").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                //minDate: 0,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });
            $("#target").keyup(function () {
                if ($("[id *=target]").val() != "") {
                    $("[id *=ContentPlaceHolder1_gvLeadList]").children
                    ('tbody').children('tr').each(function () {
                        $(this).show();
                    });
                    $("[id *=ContentPlaceHolder1_gvLeadList]").children
                    ('tbody').children('tr').each(function () {
                        var match = false;
                        $(this).children('td').each(function () {
                            if ($(this).text().toUpperCase().indexOf($("[id *=target]").val().toUpperCase()) > -1) {
                                match = true;
                                return false;
                            }
                        });
                        if (match) {
                            $(this).show();
                            $(this).children('th').show();
                        }
                        else {
                            $(this).hide();
                            $(this).children('th').show();
                        }
                    });


                    $("[id *=ContentPlaceHolder1_gvLeadList]").children('tbody').
                            children('tr').each(function (index) {
                                if (index == 0)
                                    $(this).show();
                            });
                }
                else {
                    $("[id *=ContentPlaceHolder1_gvLeadList]").children('tbody').
                            children('tr').each(function () {
                                $(this).show();
                            });
                }
            });
            $("#targetAssigned").keyup(function () {
                if ($("[id *=targetAssigned]").val() != "") {
                    $("[id *=ContentPlaceHolder1_gvAssignedList]").children
                    ('tbody').children('tr').each(function () {
                        $(this).show();
                    });
                    $("[id *=ContentPlaceHolder1_gvAssignedList]").children
                    ('tbody').children('tr').each(function () {
                        var match = false;
                        $(this).children('td').each(function () {
                            if ($(this).text().toUpperCase().indexOf($("[id *=targetAssigned]").val().toUpperCase()) > -1) {
                                match = true;
                                return false;
                            }
                        });
                        if (match) {
                            $(this).show();
                            $(this).children('th').show();
                        }
                        else {
                            $(this).hide();
                            $(this).children('th').show();
                        }
                    });


                    $("[id *=ContentPlaceHolder1_gvAssignedList]").children('tbody').
                            children('tr').each(function (index) {
                                if (index == 0)
                                    $(this).show();
                            });
                }
                else {
                    $("[id *=ContentPlaceHolder1_gvAssignedList]").children('tbody').
                            children('tr').each(function () {
                                $(this).show();
                            });
                }
            });


            $("#ContentPlaceHolder1_txtEReturn").prop('disabled', true);
            $('#ContentPlaceHolder1_txtEDepart').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtEReturn").prop('disabled', false);
                    $("#ContentPlaceHolder1_txtEReturn").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#ContentPlaceHolder1_txtEReturn").datepicker("option", "minDate", date)
                }
            });
            $("#ContentPlaceHolder1_txtEReturn").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });

            $("#ContentPlaceHolder1_txtEReminder").datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy'
            });

            CKEDITOR.disableAutoInline = true;
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtMailTemp', {
                toolbar:
            [
                { name: 'basicstyles', items: ['Bold', 'Italic'] },
                { name: 'paragraph', items: ['NumberedList', 'BulletedList'] },
                { name: 'tools', items: ['Maximize', '-', 'About'] }
            ],
                height: '300px'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdfQuoteUrl" runat="server" />
    <div class="outter-wp">
        <!--/sub-heard-part-->
        <div class="row">
            <div class="col-lg-8">
                <asp:ImageButton ID="imgbtnAddLead" ImageUrl="~/images/add-lead.png" runat="server" OnClick="imgbtnAddLead_Click1" />
            </div>
            <div class="col-lg-4">
                <%-- <div class="col-lg-3 LeadColor"></div>
                <strong>Existing Customer</strong>--%>
            </div>
        </div>
        <div class="forms-main" id="newlead" runat="server">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <div class="col-md-3">
                            <label class="control-label">Lead Source</label>
                            <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSource" ForeColor="#d0582e"
                                ErrorMessage="Please select Source" ValidationGroup="Consultant" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3" id="others" runat="server">
                            <label class="control-label">Others</label>
                            <asp:TextBox ID="txtOthers" runat="server" class="form-control" placeholder="Description"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOthers" runat="server" ControlToValidate="txtOthers" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Others description" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form">
                        <div class="col-md-3">
                            <label class="control-label">First Name</label>
                            <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                                ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Last Name</label>
                            <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Mobile</label>
                            <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form">
                        <div class="col-md-3">
                            <label class="control-label">From City</label>
                            <asp:TextBox ID="txtSource" class="form-control" runat="server" placeholder="Source"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSource" runat="server" ControlToValidate="txtSource" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Source" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">To City</label>
                            <asp:TextBox ID="txtDestination" class="form-control" runat="server" placeholder="Destination"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ControlToValidate="txtDestination" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Destination" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Depart</label>
                            <asp:TextBox ID="txtDepart" class="form-control" runat="server" placeholder="dd-mm-yyyy" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDepart" runat="server" ControlToValidate="txtDepart" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Depart Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Return</label>
                            <asp:TextBox ID="txtReturnDate" class="form-control" runat="server" placeholder="dd-mm-yyyy" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form">
                        <div class="col-md-1">
                            <label class="control-label">Adult</label>
                            <asp:DropDownList ID="ddlAdults" class="form-control" runat="server" Style="padding: 0px">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label">Child</label>
                            <asp:DropDownList ID="ddlChild" class="form-control" runat="server" Style="padding: 0px">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label">Infant</label>
                            <asp:DropDownList ID="ddlInfant" class="form-control" runat="server" Style="padding: 0px">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Product</label>
                            <asp:DropDownList ID="ddlPackage" class="form-control" runat="server" Style="padding: 0px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage" ForeColor="#d0582e"
                                ErrorMessage="Please Select Product" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Estimated Budget</label>
                            <asp:TextBox ID="txtBudget" runat="server" class="form-control" placeholder="Price" Text="0"></asp:TextBox>
                            <%--  <asp:RequiredFieldValidator ID="rfvBudget" runat="server" ControlToValidate="txtBudget" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Budget" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <div class="col-md-3 form-group button-2">
                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="~/images/Save.png" ValidationGroup="Consultant" Height="35px" />
                        <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/Update.png" OnClick="btnUpdate_Click" ValidationGroup="Consultant" Height="35px" />
                        <asp:ImageButton ID="btnReset" runat="server" OnClick="btnReset_Click" ImageUrl="~/images/Back.png" Height="35px" />
                    </div>

                    <div class="clearfix"></div>
                </div>
            </div>
        </div>

        <div class="forms-main" id="dvEdit" runat="server">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <div class="col-md-12 text-center">
                            <asp:Label ID="MailMessage" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-6" style="border: 1px black dashed; padding: 15px 0px;">
                            <div class="col-md-12 text-center">
                                <asp:Label ID="lblFollowup" runat="server"></asp:Label>
                            </div>
                            <div class="vali-form">
                                <div class="col-md-6">
                                    <label class="control-label">Lead Source</label>
                                    <asp:DropDownList ID="ddlESource" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlESource_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlESource" runat="server" ControlToValidate="ddlESource" ForeColor="#d0582e"
                                        ErrorMessage="Please Select Source" ValidationGroup="LeadEdit" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6" id="dvEOthers" runat="server">
                                    <label class="control-label">Others</label>
                                    <asp:TextBox ID="txtEOthers" runat="server" class="form-control" placeholder="Description" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEOthers" runat="server" ControlToValidate="txtEOthers" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Others Description" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="vali-form" id="status" runat="server">
                                <div class="col-md-4">
                                    <label class="control-label">Lead Status</label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                                        ErrorMessage="Please select status" ValidationGroup="LeadEdit" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="followupdate" runat="server">
                                    <label class="control-label">Follow up Date</label>
                                    <asp:TextBox ID="txtFollowUp" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFollowupdate" runat="server" ControlToValidate="txtFollowUp" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Follow up Date" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="desc" runat="server">
                                    <label class="control-label">Description</label>
                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" placeholder="Description" MaxLength="200"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">First Name</label>
                                <asp:TextBox ID="txtEFirstName" runat="server" class="form-control" placeholder="Given Name" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEFirstName" runat="server" ControlToValidate="txtEFirstName" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter First Name" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">Last Name</label>
                                <asp:TextBox ID="txtELastName" runat="server" class="form-control" placeholder="Surname" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtELastName" runat="server" ControlToValidate="txtELastName" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Last Name" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">Email</label>
                                <asp:TextBox ID="txtEEmail" runat="server" class="form-control" placeholder="Email" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEEmail" runat="server" ControlToValidate="txtEEmail" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Email" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgtxtEEmail" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                    ControlToValidate="txtEEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="LeadEdit">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">Tel</label>
                                <asp:TextBox ID="txtEMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEMobile" runat="server" ControlToValidate="txtEMobile" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Mobile" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgtxtEMobile" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                    ControlToValidate="txtEMobile" ForeColor="#d0582e" ValidationGroup="LeadEdit"></asp:RegularExpressionValidator>
                            </div>

                            <div class="col-md-6" id="dvClientFileId" runat="server">
                                <label class="control-label">Client file Id: </label>
                                <asp:TextBox ID="txtClientFileId" runat="server" class="form-control" placeholder="Client file Id" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvClientFileID" runat="server" ControlToValidate="txtClientFileId" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter File ID" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-12" style="margin-top: 15px;">
                                <label class="control-label"><strong>Final Travel Dates</strong></label>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">Depart</label>
                                <asp:TextBox ID="txtEDepart" class="form-control" runat="server" placeholder="dd-mm-yyyy" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtEDepart" runat="server" ControlToValidate="txtEDepart" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Depart Date" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">Return</label>
                                <asp:TextBox ID="txtEReturn" class="form-control" runat="server" placeholder="dd-mm-yyyy" autocomplete="off"></asp:TextBox>
                            </div>



                            <div class="col-md-12">
                                <label class="control-label">Consultant Notes</label>
                                <asp:TextBox ID="txtEConsultNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine" MaxLength="200"></asp:TextBox>

                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12" style="margin-top: 15px;">
                                <label class="control-label" style="float: left;">Set Reminder</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEReminder" runat="server" class="form-control" placeholder="dd-mm-yyyy" MaxLength="10" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12">
                                <label class="control-label">Reminder Notes</label>
                                <asp:TextBox ID="txtERemindNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                            </div>
                            <div class="col-md-12 text-center" style="margin-top: 15px;">
                                <asp:ImageButton ID="imgEUpdate" runat="server" OnClick="imgEUpdate_Click" ImageUrl="~/images/Update.png" ValidationGroup="LeadEdit" Height="35px" />
                                <asp:ImageButton ID="imgECancel" runat="server" OnClick="imgECancel_Click" ImageUrl="~/images/Back.png" Height="35px" />
                            </div>

                        </div>

                        <div class="col-md-6">
                            <div class="col-md-12" style="border: 1px black dashed; padding: 15px;">
                                <label class="control-label"><strong>LEAD DATA</strong> </label>

                                <table>
                                    <tr>
                                        <td>
                                            <label class="control-label">Client Name: </label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:Label ID="lblLName" runat="server"></asp:Label></strong></label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label">Email: </label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:Label ID="lblLEmail" runat="server"></asp:Label></strong></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label">Travel Dates:</label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:Label ID="lblLDates" runat="server"></asp:Label></strong></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label">Budget:</label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:Label ID="lblLBudget" runat="server"></asp:Label></strong></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label">Tel:</label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:Label ID="lblLPhone" runat="server"></asp:Label></strong></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label">URL:</label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:HyperLink ID="lnkUrl" runat="server" Target="_blank"></asp:HyperLink>
                                                    <%--<asp:Label ID="lblLUrl" runat="server"></asp:Label>--%></strong></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label">Notes:</label>
                                        </td>
                                        <td>
                                            <label class="control-label">
                                                <strong>
                                                    <asp:Label ID="lblLNotes" runat="server"></asp:Label></strong></label>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <div class="col-md-12" style="border: 1px black dashed; padding: 15px; margin-top: 10px;">
                                <div class="col-md-6">
                                    <label class="control-label">
                                        <button type="button" class="btn btn-info btn-sm" style="padding: 0px; margin: 0px;" data-toggle="modal" data-target="#EmailModal">
                                            <img src="images/button_send-more-info (1).png" style="height: 35px;" /></button>
                                    </label>
                                </div>
                            </div>
                            <div class="col-md-12" style="border: 1px black dashed; padding: 10px; margin-top: 15px;">

                                <div class="col-md-2">
                                    <label class="control-label"><strong>Quote</strong> </label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlQuoteDetails" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlQuoteDetails_SelectedIndexChanged" runat="server" Style="padding: 10px;">
                                        <asp:ListItem Value="1">Quote from New</asp:ListItem>
                                        <asp:ListItem Value="2">Quote from Template</asp:ListItem>
                                        <asp:ListItem Value="3">Quote Custom</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-5" runat="server" id="dvTemplates" visible="false">
                                    <asp:DropDownList ID="ddlTemplateNames" runat="server" CssClass="form-control" Style="padding: 10px;"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqfddlTemplateNames" runat="server" ControlToValidate="ddlTemplateNames" ForeColor="#d0582e"
                                        ErrorMessage="Please select Template" ValidationGroup="TemplateName" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:ImageButton ID="imgQuoteSubmit" runat="server" ImageUrl="~/images/button_generate-quote (1).png" Height="35px" ValidationGroup="TemplateName" OnClick="imgQuoteSubmit_Click" />
                                </div>


                                <%--<div class="col-md-6">
                                        <strong>QUOTE TO BOOKING</strong> <br />
                                        <strong>GENERATE INVOICE</strong><br />
                                        <strong>ISSUE VOUCHER</strong>                                       
                                    </div>--%>
                            </div>

                        </div>

                        <div class="col-md-12" id="dvHistory" style="margin-top: 15px;">

                            <%--<asp:PlaceHolder ID="HistoryPlaceholder" runat="server" />--%>

                             <div class="tables">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvHistory" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                EmptyDataText="There are no data records to display." 
                                                PageSize="100" OnRowCommand="gvHistory_RowCommand" OnRowDataBound="gvHistory_RowDataBound" OnRowEditing="gvHistory_RowEditing"
                                                Style="font-size: 110%;" ForeColor="Black">
                                                <PagerStyle CssClass="pagination_grid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Lead History" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server"  Text='<%#Eval("HistoryDesc") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server"  Text='<%#Eval("EventDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="QuoteNumber" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblHistoryQuote" Text='<%#Eval("ViewData") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="View" ID="btnViewHistory" runat="server" ToolTip="View" >View</asp:LinkButton>                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   
                                                     <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton CommandName="Edit" ID="btnEditHistory" runat="server" ToolTip="Edit" >Edit</asp:LinkButton>                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>


                        </div>

                        <style>
                            #dvHistory table tr th {
                                padding: 10px;
                            }

                            #dvHistory table tr td {
                                padding: 10px;
                            }
                        </style>

                    </div>
                    <div class="clearfix"></div>

                </div>
            </div>
        </div>



        <div class="forms-main" id="actions" runat="server">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <div class="col-md-3">
                            <label class="control-label">Assign Lead</label>
                            <asp:DropDownList ID="ddlAssignLead" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlAssignLead_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAssignLead" runat="server" ControlToValidate="ddlAssignLead" ForeColor="#d0582e"
                                ErrorMessage="Please select one Assign Option" ValidationGroup="Assign" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3" id="consultant" runat="server">
                            <label class="control-label">Consultant</label>
                            <asp:DropDownList ID="ddlConsultants" runat="server" Style="padding: 0px" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlConsultants" runat="server" ControlToValidate="ddlConsultants" ForeColor="#d0582e"
                                ErrorMessage="Please select Consultant" ValidationGroup="Assign" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form" style="margin-top: 20px;">
                        <div class="col-md-3">
                            <asp:ImageButton ID="imgbtnSubmitAssign" runat="server" OnClick="imgbtnSubmitAssign_Click" ImageUrl="~/images/Save.png" ValidationGroup="Assign" Height="35px" />
                            <asp:ImageButton ID="imgbtnBackAssign" runat="server" OnClick="imgbtnBackAssign_Click" ImageUrl="~/images/Back.png" Height="35px" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>


        <!--/sub-heard-part-->
        <!--/tabs-->
        <div class="tab-main" id="LeadList" runat="server">
            <!--/tabs-inner-->
            <div class="tab-inner">
                <div id="tabs" class="tabs">
                    <div class="graph" style="padding: 0em 0em">
                        <nav style="text-align: left;">
                            <ul>
                                <li><a href="#section-1" class="icon-shop"><span>Un-Assigned Leads</span></a></li>
                                <li><a href="#section-2" class="icon-cup"><span>Assigned Leads</span></a></li>
                            </ul>
                        </nav>
                        <div class="content tab">
                            <section id="section-1">
                                <div class="graph">
                                    <div class="row" id="search" runat="server">
                                        <div class="col-lg-12">
                                            <div class="col-lg-1 form-group">
                                                <asp:DropDownList CssClass="form-control" ID="DropPage" runat="server" Style="padding: 0px" OnSelectedIndexChanged="DropPage_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2 form-group">
                                                <label class="control-label">
                                                    Records per page</label>
                                            </div>
                                            <div class="col-lg-6 form-group">
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <input id="target" type="text" class="form-control" placeholder="Text To Search" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tables">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvLeadList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                EmptyDataText="There are no data records to display. Please Add Lead." AllowPaging="true"
                                                PageSize="100" OnRowCommand="gvLeadList_RowCommand" OnPageIndexChanging="gvLeadList_PageIndexChanging" OnRowDataBound="gvLeadList_RowDataBound"
                                                Style="font-size: 110%;" ForeColor="Black">
                                                <PagerStyle CssClass="pagination_grid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Quote" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblQuote" Text='<%#Eval("Quote") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QuoteNumber" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblQuoteNumber" Text='<%#Eval("QuoteNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblID" Text='<%#Eval("lsId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDuplicateLeadList" Text='<%#Eval("lsDuplicateLead") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Source">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsSourceRef" Text='<%#Eval("lsSourceRef") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsConsultantName" Text='<%#Eval("ConsultantName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SourceID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsSource" Text='<%#Eval("lsSource") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Others" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsOthersInfo" Text='<%#Eval("lsOthersInfo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblName" Text='<%#Eval("lsFirstName") + " " +Eval("lsLastName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="First Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("lsFirstName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLastName" Text='<%#Eval("lsLastName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblMobile" Text='<%#Eval("lsPhone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblEmailID" Text='<%#Eval("lsEmailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lsLeadActionsID" Text='<%#Eval("LeadActionsID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProdType" Text='<%#Eval("ProductType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProdID" Text='<%#Eval("lsProdType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Source">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblOrigin" Text='<%#Eval("lsOriginName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Destination">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDestination" Text='<%#Eval("lsDestinationName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Depart Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDepartDate" Text='<%#Eval("lsDepartureDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Return Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblReturnDate" Text='<%#Eval("lsReturnDate", "{0:dd-MM-yyyy}").ToString() == ""? "NA": Eval("lsReturnDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoofAdults" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdult" Text='<%#Eval("lsAdults") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoofChild" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblChildren" Text='<%#Eval("lsChildren") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoofInfants" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblInfants" Text='<%#Eval("lsInfants") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budget" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblBudget" Text='<%#Eval("lsBudget") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblNotes" Text='<%#Eval("lsNotes") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quoted Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblQuotedPrice" Text='<%#Eval("lsQuotedPrice") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinalPrice" Text='<%#Eval("lsFinalPrice") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CreatedBy" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCreatedBy" Text='<%#Eval("lsCreatedBy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned To">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAssignedto" Text='<%#Eval("AssignedTo").ToString() == "" ? "Not assigned to anyone" : Eval("AssignedTo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assigned By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAssignedby" Text='<%#Eval("AssignedBy").ToString() == "" ? "Not assigned by anyone" : Eval("AssignedBy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("LeadStatusAction") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created On">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsCreatedOn" Text='<%#Eval("lsCreatedOn", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit-user.png"
                                                                CommandName="EditLead" ToolTip="Edit" Visible="false" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/garbage.png"
                                                                CommandName="DeleteLead" ToolTip="Delete" Visible="false" />
                                                            <asp:ImageButton ID="imgbtnStaus" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Status1.png"
                                                                CommandName="Action" ToolTip="Pickup/Assign" />
                                                            <%-- <asp:ImageButton ID="imgbtnQuote" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Quote.png"
                                                                CommandName="Quote" ToolTip="Generate Quote" />
                                                            <asp:ImageButton ID="imgbtnPDF" runat="server" Width="23px" Height="23px" ImageUrl="~/images/PDFIcon.png"
                                                                CommandName="PDF" ToolTip="Download Quote" Visible="false" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </section>
                            <section id="section-2">
                                <div class="graph">
                                    <div class="row" id="assignedLeadList" runat="server">
                                        <div class="col-lg-12">
                                            <div class="col-lg-1 form-group">
                                                <asp:DropDownList CssClass="form-control" ID="ddlAssignedList" runat="server" Style="padding: 0px" OnSelectedIndexChanged="ddlAssignedList_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2 form-group">
                                                <label class="control-label">
                                                    Records per page</label>
                                            </div>
                                            <div class="col-lg-6 form-group">
                                            </div>
                                            <div class="col-lg-3 form-group">
                                                <input id="targetAssigned" type="text" class="form-control" placeholder="Text To Search" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="tables">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvAssignedList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                EmptyDataText="There are no data records to display. Please Add Lead." AllowPaging="true"
                                                PageSize="100" OnRowCommand="gvAssignedList_RowCommand" OnPageIndexChanging="gvAssignedList_PageIndexChanging" OnRowDataBound="gvAssignedList_RowDataBound"
                                                Style="font-size: 110%;" ForeColor="Black">
                                                <PagerStyle CssClass="pagination_grid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Quote" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblQuote" Text='<%#Eval("Quote") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QuoteNumber" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblQuoteNumber" Text='<%#Eval("QuoteNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblID" Text='<%#Eval("lsId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDuplicateLead" Text='<%#Eval("lsDuplicateLead") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Source">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsSourceRef" Text='<%#Eval("lsSourceRef") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsConsultantName" Text='<%#Eval("ConsultantName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SourceID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsSource" Text='<%#Eval("lsSource") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Others" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsOthersInfo" Text='<%#Eval("lsOthersInfo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblName" Text='<%#Eval("lsFirstName") + " " +Eval("lsLastName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="First Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("lsFirstName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Last Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLastName" Text='<%#Eval("lsLastName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblMobile" Text='<%#Eval("lsPhone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblEmailID" Text='<%#Eval("lsEmailId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lsLeadActionsID" Text='<%#Eval("LeadActionsID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProdType" Text='<%#Eval("ProductType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProdID" Text='<%#Eval("lsProdType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Source">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblOrigin" Text='<%#Eval("lsOriginName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Destination">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDestination" Text='<%#Eval("lsDestinationName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Depart Date">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDepartDate" Text='<%#Eval("lsDepartureDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Return Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblReturnDate" Text='<%#Eval("lsReturnDate", "{0:dd-MM-yyyy}").ToString() == ""? "NA": Eval("lsReturnDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoofAdults" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdult" Text='<%#Eval("lsAdults") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoofChild" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblChildren" Text='<%#Eval("lsChildren") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NoofInfants" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblInfants" Text='<%#Eval("lsInfants") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budget" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblBudget" Text='<%#Eval("lsBudget") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblNotes" Text='<%#Eval("lsNotes") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quoted Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblQuotedPrice" Text='<%#Eval("lsQuotedPrice") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblFinalPrice" Text='<%#Eval("lsFinalPrice") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CreatedBy" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCreatedBy" Text='<%#Eval("lsCreatedBy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Assigned To">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAssignedto" Text='<%#Eval("AssignedTo").ToString() == "" ? "Not assigned to anyone" : Eval("AssignedTo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assigned By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAssignedby" Text='<%#Eval("AssignedBy").ToString() == "" ? "Not assigned by anyone" : Eval("AssignedBy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("LeadStatusAction") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created On">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbllsCreatedOn" Text='<%#Eval("lsCreatedOn", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ProductID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblProductID" Text='<%#Eval("ProductID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit-user.png"
                                                                CommandName="EditLead" ToolTip="Edit" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/garbage.png"
                                                                CommandName="DeleteLead" ToolTip="Delete" Visible="false" />
                                                            <%--<asp:ImageButton ID="imgbtnStaus" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Status1.png"
                                                                CommandName="Action" ToolTip="Actions" />--%>
                                                            <asp:ImageButton ID="imgbtnQuote" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Quote.png"
                                                                CommandName="Quote" ToolTip="Generate Quote" Visible="false" />
                                                            <asp:ImageButton ID="imgbtnPDF" runat="server" Width="23px" Height="23px" ImageUrl="~/images/PDFIcon.png"
                                                                CommandName="PDF" ToolTip="Download Quote" Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </section>
                        </div>
                        <!-- /content -->
                    </div>
                    <!-- /tabs -->

                </div>
                <script src="js/cbpFWTabs.js"></script>
                <script>
                    new CBPFWTabs(document.getElementById('tabs'));
                </script>

            </div>
        </div>
        <!--//tabs-inner-->
    </div>

    <div class="modal fade" id="EmailModal" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h5 class="modal-title">Email Template</h5>
                </div>
                <div class="modal-body">

                    <div class="col-md-12">

                        <div class="col-md-4">
                            <label class="control-label">To</label>
                            <asp:TextBox ID="txtToEmail" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqftxtToEmail" runat="server" ControlToValidate="txtToEmail" ForeColor="#d0582e"
                                ErrorMessage="Please Email Id" ValidationGroup="Email" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">CC</label>
                            <asp:TextBox ID="txtCCEmail" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Subject</label>
                            <asp:TextBox ID="txtEmailSubject" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqftxtEmailSubject" runat="server" ControlToValidate="txtEmailSubject" ForeColor="#d0582e"
                                ErrorMessage="Please Email Subject" ValidationGroup="Email" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-12 text-center" style="margin-top: 15px;">
                        <asp:TextBox ID="txtMailTemp" runat="server" TextMode="MultiLine" ValidationGroup="Email"></asp:TextBox>
                        <asp:Button ID="btnSendMail" Text="Send Mail" runat="server" OnClick="btnSendMail_Click" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Alert!</h2>
                </div>
                <div class="modal-body">
                    <asp:Label ID="message" runat="server"></asp:Label>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="delete" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content" style="height: 150px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Delete Lead!</h2>
                </div>
                <div class="modal-body">
                    <div class="col-md-12 form-group user-form-group">
                        <asp:Label ID="lbldeletemessage" runat="server" class="control-label" />
                        <div class="pull-right">
                            <asp:Button ID="btnSure" runat="server" Text="YES" CssClass="btn btn-add btn-sm btn-primary" OnClick="btnSure_Click" Style="margin-top: 0em;"></asp:Button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        $("#ContentPlaceHolder1_txtMobile,#ContentPlaceHolder1_txtBudget").bind('keypress', function (e) {
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
        $("#ContentPlaceHolder1_txtMobile,#ContentPlaceHolder1_txtBudget").bind('mouseenter', function (e) {
            var val = $(this).val();
            if (val != '0') {
                //  val = val.replace(/[^0-9]+/g, "");
                $(this).val(val);
            }
        });
        $('#ContentPlaceHolder1_txtBudget').on('change', function () {
            if (this.value == "")
                this.value = 0;
            else
                this.value = parseFloat(this.value).toFixed(2);
        });
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openDeleteModal() {
            $('#delete').modal('show');
        }
    </script>
</asp:Content>

