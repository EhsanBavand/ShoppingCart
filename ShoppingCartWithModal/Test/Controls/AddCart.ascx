using System.Web.UI.UserControl
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCart.ascx.cs" Inherits="Test.Controls.AddCart" %>

<div class="modal-dialog" role="document">
    <div class="modal-content">

        <input id="hdProductID" runat="server" type="hidden" value="0" />
        <input id="hdQuantity" runat="server" type="hidden" value="0" />

        <div id="headerDiv" class="modal-header">
            <asp:Button ID="BtnClose" runat="server" Text="&times;" CssClass="close" OnClick="BtnClose_Click" />
            <h4 class="modal-title">Add to Cart</h4>
        </div>

        <div id="mainDiv" class="modal-body">
            <asp:DataList ID="modalContent" runat="server">
                <ItemTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-xs-3 col-sm-2 col-md-2">
                                        <center>
<img src="<%#Eval("Product.ImagePath") %>" alt="<%#Eval("Product.ProductName")%>" width="100" />
                                        <p style="display: inline"><b><%#Eval("Product.ProductName") %> </b></p>
                                                                            </center>
                                    </div>
                                    <div class="col-xs-5 col-sm-6 col-md-4">
                                        <p><%#Eval("Product.Description") %></p>
                                    </div>
                                    <div class="col-xs-3 col-sm-2 col-md-2">
                                        <center>
                                            <span>Price</span>
                                        <br />
                                        <p><%#Eval("Product.UnitPrice") %></p>
                                                                            </center>
                                    </div>
                                    <div class="col-xs-2 col-sm-2 col-md-2">
                                        <center>
                                            <span>Quantity</span>
                                        <p><%#Eval("Quantity") %></p>
                                        </center>
                                    </div>
                                    <div class="col-xs-3 col-sm-2 col-md-2">
                                        <center>
                                            <span>Total</span>
                                        <p><%#Eval("Total") %></p>
                                                                            </center>
                                    </div>
                                </div>
                            </div>
                </ItemTemplate>
            </asp:DataList>
        </div>

        <div id="footerDiv" class="modal-footer">
            <b>Subtotal(
                        <asp:Label ID="LblSubQuantity" runat="server" Text=""></asp:Label>
                items):&emsp;&emsp;   
                        <asp:Label ID="LblSubAmount" runat="server" Text="Label"></asp:Label>
            </b>
            <br />
            <br />

            <asp:Button ID="BtnShopping" runat="server" Text="Continue Shopping" class="btn btn-primary" OnClick="BtnShopping_Click" />
            <asp:Button ID="BtnViewCart" runat="server" Text="View Shopping Cart" class="btn btn-primary" OnClick="BtnViewCart_Click" />
        </div>

    </div>
</div>
