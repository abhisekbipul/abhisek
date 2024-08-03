<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyTickets.aspx.cs" Inherits="Tickets.MyTickets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <h2>My Tickets</h2>
    <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="RaisedBy" HeaderText="Raised By" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="ResolvedDate" HeaderText="Resolved Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
        </Columns>
    </asp:GridView>

</asp:Content>
