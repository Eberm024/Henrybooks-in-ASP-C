<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewTable.aspx.cs" Inherits="HenryBooks.ViewTable" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Henry Books</h1>
        <p class="lead"> Edgar Bermudez</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>View <% CurrentViewName();%>
            </h2>
            <asp:DropDownList ID="DropDownTableList" runat="server" OnSelectedIndexChanged="DropDownTableList_SelectedIndexChanged">
                    <asp:ListItem>No table selected...</asp:ListItem>
                    <asp:ListItem>Book</asp:ListItem>
                    <asp:ListItem>Publisher</asp:ListItem>
                    <asp:ListItem>Author</asp:ListItem>
                    <asp:ListItem>Copy</asp:ListItem>
                    <asp:ListItem>Branch</asp:ListItem>
                    <asp:ListItem>Wrote</asp:ListItem>
                    <asp:ListItem>Inventory</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btn_View" runat="server" Text="View" OnClick="btn_View_Click" />
             <p>
               <!-- Place mysql table html code in here...
                   make sure dropdownlist selection modifies
                   current table view-->
                 <% TableView(); %>
            </p>
            

            <asp:Button ID="btn_Goback" runat="server" Text="Go Back" OnClick="btn_GoBack_Click"/>
        </div>
    </div>

</asp:Content>
