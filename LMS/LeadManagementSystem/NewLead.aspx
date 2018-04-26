<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="NewLead.aspx.cs" Inherits="NewLead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="js/jquery-2.1.4.min.js"></script>
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <!--/sub-heard-part-->
        <div class="sub-heard-part">
            <ol class="breadcrumb m-b-0">
                <li><a>Consultant</a></li>
                <li class="active">New Consultant</li>
            </ol>
        </div>
        <!--/sub-heard-part-->
        <!--/forms-->
        <div class="forms-main">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <div class="col-md-3">
                            <label class="control-label">Lead Source</label>
                            <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged1">
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
                            <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form">
                        <div class="col-md-3">
                            <label class="control-label">From</label>
                            <asp:TextBox ID="txtSource" class="form-control" runat="server" placeholder="Source"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSource" runat="server" ControlToValidate="txtSource" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Source" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">To</label>
                            <asp:TextBox ID="txtDestination" class="form-control" runat="server" placeholder="Destination"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ControlToValidate="txtDestination" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Destination" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Depart</label>
                            <asp:TextBox ID="txtDepart" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDepart" runat="server" ControlToValidate="txtDepart" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Depart Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Return</label>
                            <asp:TextBox ID="txtReturnDate" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
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
                            <label class="control-label">Budget</label>
                            <asp:TextBox ID="txtBudget" runat="server" class="form-control" placeholder="Price"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBudget" runat="server" ControlToValidate="txtBudget" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Budget" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label">Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12 form-group button-2">

                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Consultant" Style="padding: 0.8em 1em;" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary" Style="padding: 0.8em 1em;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
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
            this.value = parseFloat(this.value).toFixed(2);
        });
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
</asp:Content>

