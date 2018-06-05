<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillForm.aspx.cs" Inherits="ADOSolution.BillForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
        <h3>Bill List</h3>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="BillID" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="BillID" HeaderText="Bill No" />
                <asp:BoundField DataField="Name" HeaderText="Customer Name" />
                <asp:BoundField DataField="BillDate" HeaderText="Date" />
                <asp:BoundField DataField="ShippingCharge" HeaderText="Shipping Charge" />
                <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" />
                <asp:BoundField DataField="IsCanceled" HeaderText="Order Cancel" />
                <asp:CommandField DeleteText="Select" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    <br/>
             <asp:Button ID="btnBack" runat="server" Text="Back" Height="35px" OnClick="btnBack_Click" Width="102px" />
    </div>
    </form>
</body>
</html>
