<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CountryForm.aspx.cs" Inherits="ADOSolution.CountryForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center">
                <thead>
                    <tr>
                        <th colspan="2">Country Form 
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCountryID" runat="server" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Country
                    </td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="margin-top: 20px">
            <asp:GridView ID="GridView1" runat="server" OnRowEditing="GridView1_RowEditing" DataKeyNames="CountryID" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="CountryID" HeaderText="CountryID" />
                    <asp:BoundField DataField="Country" HeaderText="Country" />
                    <asp:CommandField ShowEditButton="True" CausesValidation="False" InsertVisible="False" ShowCancelButton="False"  />
                    
                </Columns>
            </asp:GridView>
            
            <br/>
             <asp:Button ID="btnBack" runat="server" Text="Back" Height="35px" OnClick="btnBack_Click" Width="102px" />
        </div>
    </form>
</body>
</html>
