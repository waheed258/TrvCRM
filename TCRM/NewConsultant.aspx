<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="NewConsultant.aspx.cs" Inherits="NewConsultant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-2.1.4.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="inner_content">
        <!-- /inner_content_w3_agile_info-->
        <!-- breadcrumbs -->
        <div class="w3l_agileits_breadcrumbs">
            <div class="w3l_agileits_breadcrumbs_inner">
                <ul>
                    <li><a href="#">Dashboard</a><span>«</span></li>
                    <li>Consultants <span>«</span></li>
                    <li>New Consultant</li>
                </ul>
            </div>
        </div>
        <!-- //breadcrumbs -->
        <div class="inner_content_w3_agile_info two_in" style="margin-top:0em;">
            <div class="forms-main_agileits">
                <div class="graph-form agile_info_shadow">                    
                    <div class="form-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group col-lg-3">
                                    <label>First Name</label>
                                    <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Last Name</label>
                                    <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Mobile</label>
                                    <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                        ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group col-lg-3">
                                    <label>Login ID</label>
                                    <asp:TextBox ID="txtLoginId" class="form-control" runat="server" placeholder="Enter LoginId" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginId" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Login ID" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Create a password</label>
                                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Repeated password</label>
                                    <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" runat="server" placeholder="Confirm Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                        ControlToCompare="txtPassword" Operator="Equal" Text="Should match with Password"
                                        ErrorMessage="Should match with Password" class="validationred"
                                        ValidationGroup="Consultant" Display="Dynamic" ForeColor="#d0582e"></asp:CompareValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group col-lg-3">
                                    <label>Branch</label>
                                    <asp:DropDownList ID="ddlBranch" class="form-control" runat="server" Style="padding: 0px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ForeColor="#d0582e"
                                        ErrorMessage="Please Select Branch" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Designation</label>
                                    <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" Style="padding: 0px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation" ForeColor="#d0582e"
                                        ErrorMessage="Please Select Designation" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label>Status</label>
                                    <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" Style="padding: 0px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                                        ErrorMessage="Please Select Status" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group col-lg-3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" ValidationGroup="Consultant" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnReset_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Thank you!</h2>
                </div>
                <div class="modal-body">
                    <asp:Label ID="message" runat="server"></asp:Label>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
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
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }

    </script>
</asp:Content>

