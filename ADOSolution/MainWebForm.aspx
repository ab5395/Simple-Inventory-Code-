<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainWebForm.aspx.cs" Inherits="ADOSolution.MainWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table style="height: 469px; width: 254px">
        <tr>
            <td>
                <asp:Button ID="btnCountry" runat="server" Text="Country Form" Height="31px" Width="116px" OnClick="btnCountry_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnState" runat="server" Text="State Form" Height="31px" Width="116px" OnClick="btnState_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnCity" runat="server" Text="City Form" Height="31px" Width="116px" OnClick="btnCity_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnCustomer" runat="server" Text="Customer Form" Height="31px" Width="116px" OnClick="btnCustomer_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnProduct" runat="server" Text="Product Form" Height="31px" Width="116px" OnClick="btnProduct_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnOrder" runat="server" Text="Order Form" Height="31px" Width="116px" OnClick="btnOrder_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnBill" runat="server" Text="Bill Form" Height="31px" Width="116px" OnClick="btnBill_Click" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
