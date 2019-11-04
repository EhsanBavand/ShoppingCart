using SShoppingCart.TMP.BLL;
using SShoppingCart.TMP.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                // Fetch cart items from DB
                //using (CartRepository _repository = new CartRepository())
                CartItem _repository = new CartItem();
                {
                    List<CartItemEntity> cartItems = _repository.GetCartItems();

                    // Calculate total amount
                    double totalAmount = _repository.GetTotal();
                    // Calculate total quantity
                    double totalQuantity = _repository.GetCount();

                    // Bind cart items to Repeater control 
                    CartItemsRepeater.DataSource = cartItems;
                    CartItemsRepeater.DataBind();

                    // Set the footer part (total)
                    LblSubQuantity.Text = totalQuantity > 1 ? totalQuantity + " items" : totalQuantity + " item";
                    LblSubAmount.Text = totalAmount.ToString();
                }




            }
        }


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {


            // Get the clicked button
            Button btn = sender as Button;

            // Get product Id from command argument
            int productId = int.Parse(btn.CommandArgument);

            if (btn != null)
            {
                // Get the item of reperter control
                RepeaterItem rptItem = btn.Parent as RepeaterItem;
                if (rptItem != null)
                {
                    // Fetch data - quantity
                    TextBox tbQuantity = rptItem.FindControl("TbQuantity") as TextBox;
                    int quantity = int.Parse(tbQuantity.Text);

                    // Set the item to invisible when the quantity was set to 0
                    if (quantity == 0)
                    {
                        rptItem.Visible = false;
                    }

                    // Fetch data - price
                    Label lblPrice = rptItem.FindControl("LblPrice") as Label;
                    double price = double.Parse(lblPrice.Text);

                    // Calculate the total
                    double total = price * quantity;

                    // Fetch the total Label
                    Label lblTotal = rptItem.FindControl("LblTotal") as Label;

                    // Set the total to the label
                    lblTotal.Text = total.ToString();

                    //using (CartRepository _repository = new CartRepository())
                    CartItem _repository = new CartItem();
                    {
                        // Update quantity in the cart
                        _repository.UpdateCart(productId, quantity);

                        // Re-calculate the total, quanity & amount
                        double totalAmount = _repository.GetTotal();
                        double totalQuantity = _repository.GetCount();

                        // Set the toal amount 
                        LblSubQuantity.Text = totalQuantity > 1 ? totalQuantity + " items" : totalQuantity + " item";
                        LblSubAmount.Text = totalAmount.ToString();
                    }


                }
            }
        }

        protected void BtnShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}