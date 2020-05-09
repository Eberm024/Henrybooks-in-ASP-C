<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyTable.aspx.cs" Inherits="HenryBooks.ModifyTable" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Henry Books</h1>
        <p class="lead"> Edgar Bermudez</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Table to Modify: <% CurrentViewName(); %>
            </h2>
            <p>
               <!-- Place mysql table code in here... -->
                <% tableCreation(); %>
            </p>
            <br>
            <h4>Add new row</h4>
            <p>Format to insert data:</p> <p>item1, item2, item3, ..., itemx</p>
            <p>
            <asp:TextBox ID="tb_AddRow" runat="server" Width="277px"></asp:TextBox> <asp:Button ID="btn_AddRow" runat="server" Text="Add" OnClick="btn_AddRow_Click" />
            </p>
            <br>
            <h4>Remove row</h4>
       
            <p>Choose a Primary Key</p>
            <p>
                <asp:DropDownList ID="ddl_RemoveRow" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="-- No Selection --" Value="0" />
                </asp:DropDownList>
                <asp:Button ID="btn_RemoveConfirm" runat="server" Text="Remove" OnClick="btn_RemoveConfirm_Click" />
            </p>

            <br>
            <h4>Update Entry</h4>
            <table>
                <tr>
                <td>PrimaryKey</td> <td>Column to change</td> <td>Insert Change:</td>
                <tr>
                <td>
                    <asp:DropDownList ID="ddl_UpdateEntryPrimaryKey" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- No Selection --" Value="0" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_UpdateEntryColumn" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- No Selection --" Value="0" />
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="tb_UpdateInput" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_UpdateConfirm" runat="server" Text="Update" OnClick="btn_UpdateConfirm_Click" />
                </td>
            </table>

        </div>
    </div>
    <br>
    <br>
    <div class="Go_Back">
        <asp:Button ID="btn_GoBack" runat="server" Text="Go Back" OnClick="btn_GoBack_Click"/>
    </div>

</asp:Content>
