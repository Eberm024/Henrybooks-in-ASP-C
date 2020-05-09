<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchTitle.aspx.cs" Inherits="HenryBooks.SearchTitle" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Henry Books</h1>
        <p class="lead"> Edgar Bermudez - 5792558</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Search Results: <% displayUserTextbox(); %></h2> <br>
        
            <p> </p> <!--- result name in here --->
             <p>
               <!-- Place mysql table html code in here...
                   make sure dropdownlist selection modifies
                   current table view-->
                 <% displayTitles(); %>
            </p>
            <asp:Button ID="btn_GoBack" runat="server" Text="Go Back" OnClick="btn_GoBack_Click"/>
        </div>
    </div>

</asp:Content>
