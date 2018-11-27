<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="LeadAction.aspx.cs" Inherits="LeadAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel">
        <div class="panel-heading">
            <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Assign Lead to Consultant</h3>
        </div>
        <div class="panel-body">
            <div style="text-align: center">
                <asp:Label ID="lblMessage" runat="server" Style="font-size: 20px;"></asp:Label>
            </div>
            <div class="row">
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
            </div>
            <p></p>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmitAssign" runat="server" Text="Assign" CssClass="btn btn-primary" OnClick="btnSubmitAssign_Click" />
                    <asp:Button ID="btnBackAssign" runat="server" Text="Back" CssClass="btn btn-danger" OnClick="btnBackAssign_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

