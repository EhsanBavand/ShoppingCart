using SShoppingCart.TMP.BLL;
using SShoppingCart.TMP.DAL;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Test.Controls
{
    public partial class AddCart : System.Web.UI.UserControl
    {
        public string mainPage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["modal"] == null || Session["modal"].ToString().Equals("hide"))
            {
                CartItem cart = new CartItem();

                List<CartItemEntity> list;
                if (ProductID > 0)
                    list = cart.Select(ProductID);
                else
                    list = cart.Select(2);

                modalContent.DataSource = list;
                modalContent.DataBind();

                // Set the info for popup cart 
                LblSubQuantity.Text = "0";
                LblSubAmount.Text = "0";
            }
            else
            {
                CartItem cart = new CartItem();
                CartItemEntity cartItem = cart.AddToCart(ProductID, Quantity);

                modalContent.DataSource = new List<CartItemEntity>() { cartItem };
                modalContent.DataBind();

                // Set the info for popup cart 
                LblSubQuantity.Text = cartItem.Quantity.ToString();
                LblSubAmount.Text = cartItem.Total.ToString();
            }

            Control control = (Control)sender;
            mainPage = control.Page.ToString();
            mainPage = mainPage.Substring(4);
            mainPage = mainPage.Substring(0, mainPage.Length - 5);
        }

        protected void BtnShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/" + mainPage + ".aspx?ProductId=" + ProductID + "&Quantity=1");
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/" + mainPage + ".aspx?ProductId=" + ProductID + "&Quantity=1");
        }

        protected void BtnViewCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShoppingCart.aspx");
        }

        public int ProductID
        {
            get
            {
                var id = 0;
                int.TryParse(hdProductID.Value, out id);
                return id;
            }
            set
            {
                hdProductID.Value = Convert.ToString(value);
            }
        }

        public int Quantity
        {
            get
            {
                var qu = 0;
                int.TryParse(hdQuantity.Value, out qu);
                return qu;
            }
            set
            {
                hdQuantity.Value = Convert.ToString(value);
            }
        }
    }
}