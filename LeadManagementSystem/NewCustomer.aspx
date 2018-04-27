<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="NewCustomer.aspx.cs" Inherits="NewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="js/jquery-2.1.4.min.js"></script>
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
            $("#target").keyup(function () {
                if ($("[id *=target]").val() != "") {
                    $("[id *=ContentPlaceHolder1_gvCustomerList]").children
                    ('tbody').children('tr').each(function () {
                        $(this).show();
                    });
                    $("[id *=ContentPlaceHolder1_gvCustomerList]").children
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


                    $("[id *=ContentPlaceHolder1_gvCustomerList]").children('tbody').
                            children('tr').each(function (index) {
                                if (index == 0)
                                    $(this).show();
                            });
                }
                else {
                    $("[id *=ContentPlaceHolder1_gvCustomerList]").children('tbody').
                            children('tr').each(function () {
                                $(this).show();
                            });
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <!--/sub-heard-part-->
        <div class="row">
            <div class="col-lg-8">
                <%-- <div class="sub-heard-part">
                    <ol class="breadcrumb m-b-0">
                        <li><a>Consultant</a></li>
                        <li class="active">New Consultant</li>
                    </ol>
                </div>--%>
                <asp:ImageButton ID="imgbtnAddCustomer" ImageUrl="~/images/add-customer.png" runat="server" OnClick="imgbtnAddCustomer_Click" />
            </div>
            <div class="col-lg-4 text-right">
            </div>
        </div>

        <!--/sub-heard-part-->
        <!--/forms-->
        <div class="forms-main" id="newCustomer" runat="server">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <div class="col-md-1">
                            <label class="control-label">Title</label>
                            <asp:DropDownList ID="ddlTitle" runat="server" class="form-control" Style="padding: 0px">
                                <asp:ListItem Value="-1">-Title-</asp:ListItem>
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
                        <div class="clearfix"></div>
                    </div>


                    <div class="vali-form">
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
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form">
                        <div class="col-md-6">
                            <label class="control-label">Address</label>
                            <asp:TextBox ID="txtAddress" class="form-control" TextMode="MultiLine" runat="server" placeholder="Address"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Address" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="col-md-12 form-group button-2">
                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="~/images/Save.png" ValidationGroup="Consultant" Height="35px" />
                        <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/Update.png" OnClick="btnUpdate_Click" ValidationGroup="Consultant" Height="35px" />
                        <asp:ImageButton ID="btnReset" runat="server" OnClick="btnReset_Click" ImageUrl="~/images/Back.png" Height="35px" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>


        <div class="graph" id="customerList" runat="server">
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
                <asp:GridView ID="gvCustomerList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                    EmptyDataText="There are no data records to display."
                    AllowPaging="true" PageSize="5" OnRowCommand="gvCustomerList_RowCommand" OnPageIndexChanging="gvCustomerList_PageIndexChanging"
                    Style="font-size: 110%;" ForeColor="Black">
                    <PagerStyle CssClass="pagination_grid" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerId" Text='<%#Eval("TravellerId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerTitel" Text='<%#Eval("TravellerTitel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerFirstName" Text='<%#Eval("TravellerFirstName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerLastName" Text='<%#Eval("TravellerLastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerName" Text='<%#Eval("TravellerFirstName")+ " " + Eval("TravellerLastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email ID">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerMailId" Text='<%#Eval("TravellerMailId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TravellerPhone" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerPhone" Text='<%#Eval("TravellerPhone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerMobile" Text='<%#Eval("TravellerMobile") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TravellerAddress" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerAddress" Text='<%#Eval("TravellerAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Passport No">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTravellerPassPortNo" Text='<%#Eval("TravellerPassPortNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Passport Issue Date">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPassportIssueDate" Text='<%#Eval("PassportIssueDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Passport Expiry Date">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPassportExpiryDate" Text='<%#Eval("PassportExpiryDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CreatedBy" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCreatedBy" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UpdatedBy" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblUpdatedBy" Text='<%#Eval("UpdatedBy") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblCompanyId" Text='<%#Eval("CompanyId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit-user.png"
                                    CommandName="EditCustomer" ToolTip="Edit" />
                                <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/garbage.png"
                                    CommandName="DeleteCustomer" ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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

