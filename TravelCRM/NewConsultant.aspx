<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="NewConsultant.aspx.cs" Inherits="NewConsultant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        span {
        font-size:13px;
        }
    </style>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="validation-system">
        <div class="validation-form">
            <div class="vali-form">
                <div class="col-md-3 form-group1">
                    <label class="control-label">First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" placeholder="Given Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                        ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3 form-group1 form-last">
                    <label class="control-label">Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" placeholder="Surname"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3 form-group1 form-last">
                    <label class="control-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                    </asp:RegularExpressionValidator>
                </div>
                <div class="col-md-3 form-group1 form-last">
                    <label class="control-label">Mobile</label>
                    <asp:TextBox ID="txtMobile" runat="server" placeholder="Mobile"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                        ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="vali-form vali-form1">
                <div class="col-md-3 form-group1">
                    <label class="control-label">Login ID</label>
                    <asp:TextBox ID="txtLoginId" class="form-control" runat="server" placeholder="Enter LoginId" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginId" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Login ID" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3 form-group1">
                    <label class="control-label">Create a password</label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3 form-group1 form-last">
                    <label class="control-label">Repeated password</label>
                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" placeholder="Confirm Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="#d0582e"
                        ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        ControlToCompare="txtPassword" Operator="Equal" Text="Should match with Password"
                        ErrorMessage="Should match with Password" class="validationred"
                        ValidationGroup="Consultant" Display="Dynamic" ForeColor="#d0582e"></asp:CompareValidator>
                </div>
                <div class="col-md-3 form-group2 group-mail">
                    <label class="control-label">Branch</label>
                    <asp:DropDownList ID="ddlBranch" runat="server">
                        <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Capetown"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Durban"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ForeColor="#d0582e"
                        ErrorMessage="Please Select Branch" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="vali-form vali-form1">
                <div class="col-md-3 form-group2 group-mail">
                    <label class="control-label">Designation</label>
                    <asp:DropDownList ID="ddlDesignation" runat="server">
                        <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Manager"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Senior Consultant"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Consultant"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation" ForeColor="#d0582e"
                        ErrorMessage="Please Select Designation" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3 form-group2 group-mail">
                    <label class="control-label">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                        <asp:ListItem Value="2" Text="In-active"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                        ErrorMessage="Please Select Status" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-12 form-group">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Consultant"/>
                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-default" />
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <div class="copy" style="margin-top: 80px">
        <p>&copy; 2018. All Rights Reserved | Design by <a href="http://dinoosys.com/" target="_blank">Dinoosys Technologies</a> </p>
    </div>
      <script type="text/javascript">
              $("#ContentPlaceHolder1_txtMobile").bind('keypress', function (e) {
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
              $("#ContentPlaceHolder1_txtMobile").bind('mouseenter', function (e) {
                  var val = $(this).val();
                  if (val != '0') {
                      val = val.replace(/[^0-9]+/g, "");
                      $(this).val(val);
                  }
              });
    </script>
</asp:Content>

