<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmOrderForm.aspx.cs" Inherits="ADOSolution.ConfirmOrderForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function myFunction() {
            var x = document.getElementById("txtShippingCharge").value;
            var y = document.getElementById("txtTotal").value;
            //alert(x + "  ::  " + y);
            //alert(parseFloat(x) + parseFloat(y));
            document.getElementById("txtGrandTotal").value = parseFloat(x) + parseFloat(y);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <thead>
                    <tr>
                        <th>Bill Form</th>
                    </tr>
                </thead>
                <tr>
                    <td>
                        Customer
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCustomer" runat="server" Height="20px" Width="163px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Address
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" Height="56px" Width="187px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Country
                    </td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server" Height="20px" Width="164px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        State
                    </td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server" Height="20px" Width="164px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        City
                    </td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" Height="20px" Width="164px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Shipping Charge
                    </td>
                    <td>
                        <asp:TextBox ID="txtShippingCharge" runat="server" Height="20px" Width="164px" onchange="myFunction()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Total
                    </td>
                    <td>
                        <asp:TextBox ID="txtTotal" runat="server" Height="20px" Width="164px" ReadOnly="True"></asp:TextBox>
                        <%--<asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>--%>
                    </td>
                </tr>
                <tr style="visibility: visible">
                    <td>
                        Grand Total
                    </td>
                    <td>
                        <asp:TextBox ID="txtGrandTotal" runat="server" Height="20px" Width="164px" ReadOnly="True"></asp:TextBox>
                        <%--<asp:Label ID="lblGrandTotal" runat="server" Text="Label"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnBill" runat="server" Text="Generate Bill" Height="38px" OnClick="btnBill_Click" Width="126px"  />
                    </td>
                </tr>
            </table>
        </div>
        
        <div align="center">
            <h3>
                Order List
            </h3>
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ProductID" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            
            <br />
            <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order" Height="38px" Width="126px" OnClick="btnCancelOrder_Click"  />
        </div>
    </form>
</body>
</html>
