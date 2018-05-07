<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Quote.aspx.cs" Inherits="Quote" ValidateRequest="false" %>

<%--<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="js/jquery-2.2.3.min.js"></script>
    <script src="ckeditor/ckeditor.js"></script>
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtDate').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy'
            });

            CKEDITOR.disableAutoInline = true;
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtFlightDetails');
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtCarHireDetails');
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtHotelInfo');
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtItinerary');
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtIncludes');
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtExcludes');
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtTravelInsur', { height: '100px' });

            CKEDITOR.on('instanceReady', function () {
                $.each(CKEDITOR.instances, function (instance) {
                    CKEDITOR.instances[instance].on("change",
                        function (e) {
                            for (instance in CKEDITOR.instances)
                                CKEDITOR.instances[instance].updateElement();
                     });
                });
            });           

            $("#ContentPlaceHolder1_txtAdultPrice").blur(function () {
                var value = $(this).val();
                if (value != '')
                    $('#ContentPlaceHolder1_ddlAdultPersons').removeAttr("disabled");
                else {
                    alert("Enter Adult Price");
                    $('#ContentPlaceHolder1_ddlAdultPersons').attr("disabled", "disabled");
                }
            });

            $("#ContentPlaceHolder1_txtChildPrice").blur(function () {
                var value = $(this).val();
                if (value != '')
                    $('#ContentPlaceHolder1_ddlChildPersons').removeAttr("disabled");
                else {
                    alert("Enter Child Sharing Price");
                    $('#ContentPlaceHolder1_ddlChildPersons').attr("disabled", "disabled");
                }
            });

            $("#ContentPlaceHolder1_txtAdultPrice,#ContentPlaceHolder1_txtChildPrice").bind('keypress', function (e) {
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
        });

        function openModal() {
            $('#myModal').modal('show');
        }

    </script>

    <style>
        label {
            /*font-size: 1.5em !important;*/
            margin-top: 15px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <div class="forms-main" runat="server">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <h5>Lead Details</h5>
                        <div class="graph-form">
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="control-label">Client Name</label>
                                    <asp:Label ID="lblClientName" class="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Product</label>
                                    <asp:Label ID="lblProduct" class="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Source</label>
                                    <asp:Label ID="lblSource" class="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Destination</label>
                                    <asp:Label ID="lblDestination" class="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">Date</label>
                                <asp:TextBox ID="txtDate" class="form-control" runat="server" placeholder="DD-MM-YYYY"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Date" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="vali-form">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">Cost Type for Adult</label>
                                <asp:DropDownList ID="ddlAdultType" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlAdultType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvAdult" runat="server" ControlToValidate="ddlAdultType" ForeColor="#d0582e"
                                    ErrorMessage="Please select Cost Type" ValidationGroup="Quote" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="dvAdultPrice" runat="server">
                                <label class="control-label">Price</label>
                                <asp:TextBox ID="txtAdultPrice" runat="server" class="form-control" placeholder="Adult Price" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAdultPice" runat="server" ControlToValidate="txtAdultPrice" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Price" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="dvAdultPersons" runat="server" visible="false">
                                <label class="control-label">No Of Persons</label>
                                <asp:DropDownList ID="ddlAdultPersons" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlAdultPersons_SelectedIndexChanged">
                                    <asp:ListItem Value="0">No Of Persons</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtAdultQty" runat="server" class="form-control" placeholder="No Of Persons"></asp:TextBox>--%>
                                <asp:RequiredFieldValidator ID="rfvAdultQty" runat="server" ControlToValidate="ddlAdultPersons" ForeColor="#d0582e"
                                    ErrorMessage="Please Select No Of Persons" ValidationGroup="Quote" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" runat="server" id="dvAdultTot" visible="false">
                                <label class="control-label">Total Price</label>
                                <asp:Label ID="lblAdultTotPrice" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div>

                    <div class="vali-form">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">Cost Type for Child</label>
                                <asp:DropDownList ID="ddlChildType" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlChildType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3" id="dvChildPrice">
                                <label class="control-label">Price</label>
                                <asp:TextBox ID="txtChildPrice" runat="server" class="form-control" placeholder="Child Price" MaxLength="9"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="rfvChildPrice" runat="server" ControlToValidate="txtChildPrice" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Price" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="col-md-3" id="dvChildPersons" runat="server" visible="false">
                                <label class="control-label">No Of Childrens</label>
                                <asp:DropDownList ID="ddlChildPersons" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlChildPersons_SelectedIndexChanged">
                                    <asp:ListItem Value="0">No Of Childrens</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtChildQty" runat="server" class="form-control" placeholder="No Of Childrens"></asp:TextBox>--%>
                                <asp:RequiredFieldValidator ID="rfvChildPersons" runat="server" ControlToValidate="ddlChildPersons" ForeColor="#d0582e"
                                    ErrorMessage="Please Select No Of Childrens" ValidationGroup="Quote" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="dvChildTotalPrice" runat="server" visible="false">
                                <label class="control-label">Total Price</label>
                                <asp:Label ID="lblChildTotPrice" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="vali-form">
                        <label class="control-label">Flight Details</label>
                        <%--<CKEditor:CKEditorControl ID="txtFlightDetails" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>--%>
                        <asp:TextBox ID="txtFlightDetails" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvfFlight" runat="server" ControlToValidate="txtFlightDetails" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Flight Details" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="vali-form">
                        <label class="control-label">Car Hire</label>
                        <asp:TextBox ID="txtCarHireDetails" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvfCarHire" runat="server" ControlToValidate="txtCarHireDetails" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Car Hire Details" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="vali-form">
                        <label class="control-label">Hotel Info</label>
                        <asp:TextBox ID="txtHotelInfo" TextMode="MultiLine" runat="server" class="form-control" placeholder="No Of Childrens"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvfHotelInfo" runat="server" ControlToValidate="txtHotelInfo" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Hotel Details" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="vali-form">
                        <label class="control-label">Itinerary</label>
                        <asp:TextBox ID="txtItinerary" TextMode="MultiLine" runat="server" class="form-control" placeholder="No Of Childrens"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvItenerary" runat="server" ControlToValidate="txtItinerary" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Itenerary Details" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="vali-form">
                        <label class="control-label">Includes</label>
                        <asp:TextBox ID="txtIncludes" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvIncludes" runat="server" ControlToValidate="txtIncludes" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Includes" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="vali-form">
                        <label class="control-label">Excludes</label>
                        <asp:TextBox ID="txtExcludes" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExcludes" runat="server" ControlToValidate="txtExcludes" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Excludes" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="vali-form">
                        <label class="control-label">Travel Insurance</label>
                        <asp:TextBox ID="txtTravelInsur" TextMode="MultiLine" runat="server" class="form-control" placeholder="Travel Insurance Details" MaxLength="500" Columns="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTravelInsur" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Travel Insurance Details" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                    <div class="clearfix"></div>

                    <div class="vali-form">
                        <div class="col-md-3" style="margin-bottom: 20px; margin-top: 10px;">
                            <asp:ImageButton ID="imgbtnSubmitAssign" runat="server" ImageUrl="~/images/Save.png" OnClick="imgbtnSubmitAssign_Click" ValidationGroup="Quote" Height="35px" />
                            <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/images/clear.png" OnClick="imgbtnClear_Click" Height="35px" />
                        </div>
                        <div class="clearfix"></div>
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

</asp:Content>

