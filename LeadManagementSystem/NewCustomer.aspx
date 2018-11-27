<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="NewCustomer.aspx.cs" Inherits="NewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel">
        <div class="panel-heading">
            <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Create New Customer</h3>
        </div>
        <div class="panel-body">
            <div style="text-align: center">
                <asp:Label ID="lblMessage" runat="server" Style="font-size: 20px;"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <label class="control-label">Title</label>
                    <asp:DropDownList ID="ddlTitle" runat="server" class="form-control" Style="padding: 0px">
                        <asp:ListItem Value="-1">Title</asp:ListItem>
                        <asp:ListItem Value="0">Mr</asp:ListItem>
                        <asp:ListItem Value="1">Miss</asp:ListItem>
                        <asp:ListItem Value="2">Mrs</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle" ForeColor="#d0582e"
                        ErrorMessage="Please select Title" ValidationGroup="Consultant" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-2" id="others" runat="server">
                    <label class="control-label">First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                        ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                    </asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Mobile</label>
                    <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                        ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label class="control-label">Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" class="form-control" placeholder="Phone" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rgvPhone" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                        ControlToValidate="txtPhone" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Passport No</label>
                    <asp:TextBox ID="txtPassportNo" class="form-control" runat="server" placeholder="Passport" MaxLength="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassport" runat="server" ControlToValidate="txtPassportNo" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Passport" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Passport Issue Date</label>
                    <asp:TextBox ID="txtIssueDate" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvIssueDate" runat="server" ControlToValidate="txtIssueDate" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Issue Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Passport Expiry Date</label>
                    <asp:TextBox ID="txtExpiry" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvExpiry" runat="server" ControlToValidate="txtExpiry" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Expiry Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <label class="control-label">Address</label>
                    <asp:TextBox ID="txtAddress" class="form-control" TextMode="MultiLine" runat="server" placeholder="Address"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Address" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <p></p>
            <div class="row" style="margin-left:1px;">
                <asp:Button ID="ImageButton1" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="ImageButton1_Click" />
                <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="btnUpdate_Click" />--%>
                <asp:Button ID="btnReset" runat="server" Text="Back" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="btnReset_Click" />
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtIssueDate').datepicker({
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                changeYear: true,
                yearRange: "1930:2050",
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtExpiry").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#ContentPlaceHolder1_txtExpiry").datepicker("option", "minDate", date)
                }
            });
            $("#ContentPlaceHolder1_txtExpiry").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true,
                changeYear: true,
                yearRange: "1930:2050"
            });
        });
    </script>
</asp:Content>

