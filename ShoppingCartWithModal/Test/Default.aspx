<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Test._Default" %>

<%@ Register TagPrefix="ShoppingCart" TagName="AddCart" Src="~/Controls/AddCart.ascx" %>

<asp:Content ID="head1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var modal = '<%=modal%>';
        $(document).ready(function () {
            console.log(modal);
            $('#myModal').modal(modal);
        });
    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <asp:Repeater ID="productRepeater" runat="server">
            <ItemTemplate>
                <div class="col-xs-12 col-sm-6 col-md-4">
                    <img src="<%#Eval("ImagePath") %>" alt="<%#Eval("ProductName") %>" />
                    <br></br>
                    <h2 style="display: inline"><%#Eval("ProductName") %> </h2>
                    <h4 style="display: inline; float: right; color: red; font-weight: bold;">$<%#Eval("UnitPrice") %>/KG </h4>
                    <p><%#Eval("Description") %></p>

                    <div class="text-left">
                        <a class="btn btn-primary btn-md" href='ProductDetails.aspx?ProductId=<%#Eval("ProductID") %>'>Details</a>
                        <asp:Button runat="server" CommandArgument='<%#Eval("ProductID") %>' Text="Add to Cart" OnClick="BtnAddToCart_Click" class="btn btn-primary btn-md" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div id="myModal" class="modal fade centered-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <ShoppingCart:AddCart ID="ctlCart" runat="server" />
    </div>
</asp:Content>
