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
                <asp:ImageButton ID="imgbtnAddLead" ImageUrl="~/images/add-lead.png" runat="server" OnClick="imgbtnAddLead_Click" />
            </div>
            <div class="col-lg-4 text-right">
            </div>
        </div>

        <!--/sub-heard-part-->
        <!--/forms-->
        <div class="forms-main" id="newlead" runat="server">
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
                    <div class="vali-form" id="status" runat="server">
                        <div class="col-md-3">
                            <label class="control-label">Staus</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                                ErrorMessage="Please select status" ValidationGroup="Consultant" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3" id="followupdate" runat="server">
                            <label class="control-label">Follow up Date</label>
                            <asp:TextBox ID="txtFollowUp" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFollowupdate" runat="server" ControlToValidate="txtFollowUp" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Follow up Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3" id="desc" runat="server">
                            <label class="control-label">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" class="form-control" placeholder="Description"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="col-md-3 form-group button-2">
                        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="~/images/Save.png" ValidationGroup="Consultant" Height="35px" />
                        <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/Update.png" OnClick="btnUpdate_Click" ValidationGroup="Consultant" Height="35px" />
                        <asp:ImageButton ID="btnReset" runat="server" OnClick="btnReset_Click" ImageUrl="~/images/Back.png" Height="35px" />
                    </div>
                    <div class="col-md-9 form-group button-2">
                        <asp:Label ID="lblFollowup" runat="server"></asp:Label>
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


        <div class="graph" id="LeadList" runat="server">
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
                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsSourceRef" Text='<%#Eval("lsSourceRef") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By">
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

                            <asp:TemplateField HeaderText="Product">
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

                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAssignedby" Text='<%#Eval("AssignedBy").ToString() == "" ? "Not assigned by anyone" : Eval("AssignedBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("LeadStatusAction") %>'></asp:Label>
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
                                    <asp:ImageButton ID="imgbtnStaus" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Status1.png"
                                        CommandName="Action" ToolTip="Actions" />
                                    <asp:ImageButton ID="imgbtnQuote" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Quote.png"
                                        CommandName="Quote" ToolTip="Generate Quote" />
                                     <asp:ImageButton ID="imgbtnPDF" runat="server" Width="23px" Height="23px" ImageUrl="~/images/PDFIcon.png"
                                        CommandName="PDF" ToolTip="Download Quote" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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

