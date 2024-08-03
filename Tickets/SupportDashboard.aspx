<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupportDashboard.aspx.cs" Inherits="Tickets.SupportDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Support Dashboard</h2>
    <asp:GridView ID="gvSupportTickets" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="gvSupportTickets_RowCommand">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="RaisedBy" HeaderText="Raised By" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="Open" Value="Open" />
                        <asp:ListItem Text="In Progress" Value="In Progress" />
                        <asp:ListItem Text="Resolved" Value="Resolved" />
                    </asp:DropDownList>
                    <asp:Button ID="btnUpdate" runat="server" CommandName="UpdateStatus" CommandArgument='<%# Eval("TicketID") %>' Text="Update" CssClass="btn btn-primary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
