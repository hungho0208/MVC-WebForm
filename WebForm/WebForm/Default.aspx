<%@ Page Title="首頁" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm._Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Literal ID="litAlertMessage" runat="server" Visible="False"></asp:Literal>
    <ol class="round">
        <li class="one">
            <h3>姓名</h3>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

        </li>
        <li class="two">
            <h3>年齡</h3>
            <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>

        </li>
        <li class="three">
            <h3>生日</h3>
            <asp:TextBox ID="txtBirthday" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDate" runat="server" ControlToValidate="txtBirthday"
             ErrorMessage="請輸入有效的日期（YYYY-MM-DD）" ValidationExpression="^\d{4}-\d{2}-\d{2}$"></asp:RegularExpressionValidator>
        </li>
        
        <asp:TextBox ID="txtGuid" runat="server" Visible="False"></asp:TextBox>
          
    </ol>
    <asp:Button ID="btnSubmit" runat="server" Text="建立帳號" OnClick="btnSubmit_Click" />
    <asp:PlaceHolder ID="phButtons" runat="server"></asp:PlaceHolder>
    <asp:PlaceHolder ID="phTable" runat="server">
        <asp:Table ID="dynamicTable" runat="server"></asp:Table>
    </asp:PlaceHolder>
</asp:Content>
