<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Test.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .product_icon {
            width: 100px;
        }
    </style>
    <br />
    <br />
    <asp:UpdatePanel ID="UpdPnlCartList" runat="server">
        <ContentTemplate>
            <div id="headerDiv">
                <h4 class="modal-title">View Your Shopping Cart</h4>
                <p>&nbsp;</p>
                <h5>
                    <strong>
                        <asp:Label ID="lblCartEmpty" runat="server" Text="Your cart is empty." Visible="False"></asp:Label>
                    </strong>
                </h5>
            </div>


            <div id="mainDiv" style="left: 0px; top: 0px">

                <div class="row">
                    <div class="col-xs-3 col-sm-3 col-md-2">
                    </div>
                    <div class="col-xs-3 col-sm-3 col-md-4">
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2">
                        <span>Price</span>
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2">
                        <span>Quantity</span>
                    </div>
                    <div class="col-xs-2 col-sm-2 col-md-2">
                        <span>Total</span>
                    </div>
                </div>

                <asp:Repeater ID="CartItemsRepeater" runat="server">
                    <ItemTemplate>
                        <hr />
                        <div class="row">
                            <div class="col-xs-3 col-sm-3 col-md-2">
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Product.ImagePath") %>' Width="100px" />
                            </div>
                            <div class="col-xs-3 col-sm-3 col-md-4">
                                <b>
                                    <asp:Label ID="LblProductName" runat="server" Text='<%#Eval("Product.ProductName") %>'></asp:Label></b><br />
                                <asp:Label ID="LblDescpriton" runat="server" max-Width="350px" Text='<%#Eval("Product.Description") %>'></asp:Label>
                            </div>
                            <div class="col-xs-2 col-sm-2 col-md-2">
                                <asp:Label ID="LblPrice" runat="server" Width="100px" Text='<%#Eval("Product.UnitPrice") %>'></asp:Label></td>
                            </div>
                            <div class="col-xs-2 col-sm-2 col-md-2">
                                <br />
                                <asp:TextBox ID="TbQuantity" Text='<%#Eval("Quantity") %>' runat="server" MaxLength="3" TextMode="Number" Width="50" CssClass="text-center"></asp:TextBox>
                                <asp:Button ID="BtnUpdate" OnClick="BtnUpdate_Click" CommandArgument='<%#Eval("ProductID") %>' runat="server" Text="Update" CssClass="btn-link" />
                                <br />
                                <asp:RangeValidator ID="RvQuantity" runat="server" ControlToValidate="TbQuantity" ErrorMessage="* Quantity must be 0 or positive number." ForeColor="Red" MaximumValue="999" MinimumValue="0" Display="Static"></asp:RangeValidator>
                            </div>
                            <div class="col-xs-2 col-sm-2 col-md-2">
                                <asp:Label ID="LblTotal" runat="server" Width="100px" Text='<%#Eval("Total") %>'> </asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div id="footerDiv" class="modal-footer">
                <b>Subtotal (
                        <asp:Label ID="LblSubQuantity" runat="server" Text=""></asp:Label>
                    ):&emsp;&emsp;   
                        <asp:Label ID="LblSubAmount" runat="server" Text="Label"></asp:Label>
                </b>
                <br />
                <br />

                <asp:Button ID="BtnShopping" runat="server" Text="Continue Shopping" OnClick="BtnShopping_Click" class="btn btn-primary" />
                <asp:Button ID="BtnCheckout" runat="server" Text="Proceed to Checkout" class="btn btn-primary" />

            </div>
        </ContentTemplate>

    </asp:UpdatePanel>



</asp:Content>
