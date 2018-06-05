<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityForm.aspx.cs" Inherits="ADOSolution.CityForm" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                $("#txtCountry")
                    .change(function () {
                        $("#txtState option").remove();
                        var cid = $("#txtCountry").val();
                        $(function () {
                            $.ajax({
                                type: "POST",
                                url: "CityForm.aspx/BindState",
                                data: '{cid:' + cid + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (result) {
                                    result = result.d;
                                    $.each(result, function (key, value) {
                                        $("#txtState").append($("<option></option>").val
                                        (value.StateId).html(value.State));
                                    });
                                },
                                error: function (result) {
                                }
                            });
                        });
                    });


                $("#btnInsert").click(function () {
                    var obj = {};
                    obj.city = $("#txtCity").val();
                    obj.sid = $("#txtState").val();
                    $.ajax({
                        type: "POST",
                        url: "CityForm.aspx/AddCity",
                        data: "{'city':'" + obj.city + "','sid':'" + obj.sid + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            window.location.href = "CityForm.aspx";
                            window.location.assign("CityForm.aspx");
                        }
                    });

                });

            });


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <table align="center">
                    <thead>
                        <tr>
                            <th colspan="2">City Form 
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtCityID" runat="server" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Country
                        </td>
                        <td>
                            <asp:DropDownList ID="txtCountry" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>State
                        </td>
                        <td>
                            <asp:DropDownList ID="txtState" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>City
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                           <%-- <asp:Button  runat="server" Text="Insert" />--%>
                            <input type="button" id="btnInsert" value="Insert"/>
                        </td> 
                    </tr>
                </table>
            </div>

            <div align="center" style="margin-top: 20px">
                <asp:GridView ID="GridView1" runat="server" DataKeyNames="CityID" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="CityID" HeaderText="CityID" />
                        <asp:BoundField DataField="City" HeaderText="City" />
                        <asp:BoundField DataField="State" HeaderText="State" />
                        <asp:BoundField DataField="Country" HeaderText="Country" />

                        <asp:CommandField ShowDeleteButton="True" />

                    </Columns>
                </asp:GridView>
                
            <br/>
             <asp:Button ID="btnBack" runat="server" Text="Back" Height="35px" OnClick="btnBack_Click" Width="102px" />
            </div>
        </div>
    </form>
</body>
</html>
