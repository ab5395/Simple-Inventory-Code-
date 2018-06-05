<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="ADOSolution.ProductForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" Height="31px" Width="166px" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Height="31px" Width="166px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Quantity
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Height="31px" Width="166px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Saling MRP
                    </td>
                    <td>
                        <asp:TextBox ID="txtSalingMRP" runat="server" Height="31px" Width="166px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Is Active
                    </td>
                    <td>
                        <asp:CheckBox ID="chkStatus" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="Name" HeaderText="Product Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="SaleMRP" HeaderText="SaleMRP" />
                    <asp:BoundField DataField="IsActive" HeaderText="Status" />
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br/>
             <asp:Button ID="btnBack" runat="server" Text="Back" Height="35px" OnClick="btnBack_Click" Width="102px" />
        </div>
    </form>
</body>
</html>
