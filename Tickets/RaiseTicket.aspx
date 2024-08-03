<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RaiseTicket.aspx.cs" Inherits="Tickets.RaiseTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>&nbsp;</h2>
    <h2>&nbsp;</h2>
    <h2>Raise a New Ticket</h2>
    <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="30"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblAssignTo" runat="server" Text="Assign To:"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ddlAssignTo" runat="server"></asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="lblUpload" runat="server" Text="Upload Image:"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" BackColor="#00CC00" OnClick="btnSubmit_Click" style="height: 26px" />
    <br />
    <br />
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

</asp:Content>
