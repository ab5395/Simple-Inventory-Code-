<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderWebForm.aspx.cs" Inherits="ADOSolution.OrderWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <thead>
                    <tr>
                        <th colspan="2">Order Form
                        </th>
                    </tr>
                    <tr>
                        <td>Product
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProduct" runat="server" Height="26px" Width="205px" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Quantity
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" Height="26px" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Price
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrice" runat="server" Height="26px" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Discount
                        </td>
                        <td>
                            <asp:TextBox ID="txtDiscount" runat="server" Height="26px" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                </thead>
            </table>
            <asp:Button ID="btnAddNew" runat="server" Text="Add New Order" Height="39px" OnClick="btnAddNew_Click" />
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
            
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm Order" Height="39px" OnClick="btnConfirm_Click1" />
            
            &nbsp;&nbsp;&nbsp;
            
            <asp:Button ID="btnCancelOrder" runat="server" Text="Cancel Order" Height="39px" Width="126px" OnClick="btnCancelOrder_Click"  />
            
            <br/>
            <br/>
             <asp:Button ID="btnBack" runat="server" Text="Back" Height="39px" Width="126px" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
