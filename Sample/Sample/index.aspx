<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="index.aspx.vb" Inherits="Sample.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="outputArea">
        <asp:label ID="output" runat="server"></asp:label>
    </div>
    <div id="inputArea">
        <asp:Button runat="server" ID="btn" text="登録"/>
        <div>名前↓
        </div>
        <input type="text" id="userName" runat="server" class="textBox" />
        <div>投稿文↓</div>
        <input type="text" id="text" runat="server" class="textBox">
    </div>
</asp:Content>
