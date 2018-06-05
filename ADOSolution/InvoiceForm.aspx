<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceForm.aspx.cs" Inherits="ADOSolution.InvoiceForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h3 align="center">Invoice Data
            </h3>
            <table>
                <tr>
                    <td>Invoice(Bill) No
                    </td>
                    <td>
                        <asp:TextBox ID="txtBillNo" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Customer Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Address
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" Height="72px" Width="243px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>City
                    </td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>State
                    </td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Country
                    </td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Shipping Charge
                    </td>
                    <td>
                        <asp:TextBox ID="txtShippingcharge" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Total Product Charge
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalProductCharge" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Total Charge
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotalCharge" runat="server" Height="35px" Width="180px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div align="center" style="margin-top: 50px">
            <h3>Product Detail</h3>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <br />
            <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order" Height="38px" Width="126px" OnClick="btnCancelOrder_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Back To List" Height="38px" Width="126px" OnClick="btnBack_Click" />

        </div>
    </form>
</body>
</html>
