<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCustomerForm.aspx.cs" Inherits="ADOSolution.EditCustomerForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#ddlCountry").change(function () {
                alert($("#ddlCountry").val());
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Id
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
                    <td>Address
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="113px" Width="291px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Country
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" Height="31px" Width="166px" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>State
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server" Height="31px" Width="166px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>City
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCity" runat="server" Height="31px" Width="166px"></asp:DropDownList>
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
                        <asp:Button ID="btnUpdate" runat="server" Text="Submit" OnClick="btnUpdate_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
