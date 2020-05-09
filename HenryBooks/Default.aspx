<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HenryBooks._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Henry Books</h1>
        <p class="lead"> Edgar Bermudez - 5792558</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Table to mofidy
                <asp:DropDownList ID="ddl_ModifyTable" runat="server" OnSelectedIndexChanged="ddl_ModifyTable_SelectedIndexChanged">
                    <asp:ListItem>Book</asp:ListItem>
                    <asp:ListItem>Publisher</asp:ListItem>
                    <asp:ListItem>Author</asp:ListItem>
                    <asp:ListItem>Copy</asp:ListItem>
                    <asp:ListItem>Branch</asp:ListItem>
                    <asp:ListItem>Wrote</asp:ListItem>
                    <asp:ListItem>Inventory</asp:ListItem>
                </asp:DropDownList>
            </h2>
            <p>
                <asp:Button ID="btn_ModifyTable" runat="server" OnClick="btn_ModifyTable_Click" Text="Confirm" />
            </p>
        </div>
        <div class="col-md-4">
            <h2>View
                <asp:DropDownList ID="ddl_ViewTable" runat="server">
                    <asp:ListItem>Book</asp:ListItem>
                    <asp:ListItem>Publisher</asp:ListItem>
                    <asp:ListItem>Author</asp:ListItem>
                    <asp:ListItem>Copy</asp:ListItem>
                    <asp:ListItem>Branch</asp:ListItem>
                    <asp:ListItem>Wrote</asp:ListItem>
                    <asp:ListItem>Inventory</asp:ListItem>
                </asp:DropDownList>
            </h2>
            <p>
                <asp:Button ID="btn_ViewTable" runat="server" Text="Confirm" OnClick="btn_ViewTable_Click" />
            </p>
        </div>
        <div class="col-md-4">
            <h2>Search book by title</h2>
            <p>
                <asp:TextBox ID="tb_SearchTitle" runat="server" Width="338px"></asp:TextBox>
&nbsp; &#8195;<asp:Button ID="btn_SearchTitle" runat="server" Text="Confirm" OnClick="btn_SearchTitle_Click"/>
            </p>
        </div>
    </div>

</asp:Content>