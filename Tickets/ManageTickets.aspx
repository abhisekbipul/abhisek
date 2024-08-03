<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageTickets.aspx.cs" Inherits="Tickets.ManageTickets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style>
        .ticket-actions {
            display: flex;
            justify-content: space-between;
        }
        .ticket-actions select, .ticket-actions button {
            margin-left: 10px;
        }
    </style>

</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="NavigationContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Manage Tickets</h2>
   <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="False" OnRowCommand="gvTickets_RowCommand">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="RaisedByUsername" HeaderText="Raised By" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="Assigned To">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlAssignee" runat="server">
                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAssign" runat="server" Text="Assign" CommandName="Assign" CommandArgument='<%# Eval("TicketID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:HyperLink ID="hlImage" runat="server" NavigateUrl='<%# Eval("ImagePath") %>' Text="View/Download" Target="_blank" Visible='<%# !string.IsNullOrEmpty(Eval("ImagePath").ToString()) %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnResolve" runat="server" Text="Resolve" CommandName="Resolve" CommandArgument='<%# Eval("TicketID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

   <%-- <asp:SqlDataSource ID="TicketsDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tickets;Integrated Security=True;Multiple Active Result Sets=True %>" 
        SelectCommand="SELECT TicketID, Title, Description, Status, RaisedBy, AssignedTo FROM Tickets"
        UpdateCommand="UPDATE Tickets SET AssignedTo = @AssignedTo, Status = @Status WHERE TicketID = @TicketID">
        <UpdateParameters>
            <asp:Parameter Name="AssignedTo" Type="Int32" />
            <asp:Parameter Name="Status" Type="String" />
            <asp:Parameter Name="TicketID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="AssigneeDataSource" runat="server"
        ConnectionString="<%$ ConnectionStrings:Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tickets;Integrated Security=True;Multiple Active Result Sets=True %>"
        SelectCommand="SELECT UserID, Username FROM Users WHERE Role = 'Support'">
    </asp:SqlDataSource>--%>

</asp:Content>
