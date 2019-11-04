<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="Test.ProductDetails" %>

<%@ Register TagPrefix="ShoppingCart" TagName="AddCart" Src="~/Controls/AddCart.ascx" %>

<asp:Content ID="head2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var modal = '<%=modal%>';
        $(document).ready(function () {
            console.log(modal);
            $('#myModal1').modal(modal);
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="align-content:center; vertical-align:middle;">
        <div class="col-xs-5 col-sm-4 col-md-3">
            <asp:Image ID="ImgProduct" runat="server" max-Width="300px" />
        </div>
        <div class="col-xs-2 col-sm-2 col-md-1" display: inline-block>
            <b>
                <asp:Label ID="LblProductName" runat="server" Text='Product Name' style="align-content:center; vertical-align:middle"></asp:Label>
            </b>
        </div>
        <div class="col-xs-2 col-sm-3 col-md-2" display: inline-block>
            <b>
                <asp:Label ID="LblPrice" runat="server" max-Width="100px" Text="Product.UnitPrice" style="align-content:center; vertical-align:middle"></asp:Label>
            </b>
            <asp:TextBox ID="TbQuantity" runat="server" MaxLength="3" TextMode="Number" max-Width="50" CssClass="text-center" style="align-content:center; vertical-align:middle"></asp:TextBox>
        </div>
        <div class="col-xs-3 col-sm-3 col-md-2" display: inline-block>
            <asp:Button runat="server" Text="Add to Cart" OnClick="BtnAddToCart_Click" class="btn btn-primary btn-md" />
            <br />
            <asp:RangeValidator ID="RvQuantity" runat="server" ControlToValidate="TbQuantity" ErrorMessage="* Quantity must be positive number." ForeColor="Red" MaximumValue="999" MinimumValue="1"></asp:RangeValidator>
        </div>
        <br />
        <br />
        <br />
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12">
            <asp:Label ID="LblDescpriton" runat="server" Text="Product Description"></asp:Label>
        </div>
    </div>

    <asp:HiddenField ID="HiddenProductId" runat="server" />

    <div id="myModal1" class="modal fade centered-modal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <ShoppingCart:AddCart ID="ctlCart" runat="server" />
    </div>

</asp:Content>
