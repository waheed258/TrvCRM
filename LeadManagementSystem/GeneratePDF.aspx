<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneratePDF.aspx.cs" Inherits="GeneratePDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:TextBox ID="txtQuoteNumber" runat="server"></asp:TextBox>
            <asp:Button ID="btnGenerateQuote" runat="server" Text="GenerateSingleQuote" OnClick="btnGenerateQuote_Click" />
        </div>
        <div>
            <asp:TextBox ID="txtMultipleQuotes" runat="server"></asp:TextBox>
            <asp:Button ID="btnGenerateMultipleQuotes" runat="server" Text="GenerateMultipleQuotes" OnClick="btnGenerateMultipleQuotes_Click" />
        </div>
    </form>
</body>
</html>
