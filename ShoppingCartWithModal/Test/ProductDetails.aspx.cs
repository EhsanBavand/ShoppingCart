using SShoppingCart.TMP.BLL;
using SShoppingCart.TMP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        public string modal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["modal"] == null)
                    this.modal = "hide";
                else
                    this.modal = Session["modal"].ToString();

                // Not come from the product list page
                if (Request.QueryString["ProductId"] == null)
                {
                    // IMPLEMENTE: ERROR PAGE ???
                    // BASICALLY, NOBODY JUMP INTO THIS PAGE DIRECTLY
                    return;
                }

                // Get product id
                int productId = int.Parse(Request.QueryString["ProductId"] ?? "0");
                if (productId == 0)
                    productId = 1;

                ctlCart.ProductID = productId;

                int quantity = int.Parse(Request.QueryString["Quantity"] ?? "0");
                if (quantity == 0)
                    quantity = 1;

                ctlCart.Quantity = quantity;

                // Get product info
                var product = SShoppingCart.TMP.BLL.Product.Get(productId);

                // Set prodcut info
                ImgProduct.ImageUrl = product.ImagePath;
                LblProductName.Text = product.ProductName;
                LblPrice.Text = product.UnitPrice.ToString();
                LblDescpriton.Text = product.Description;
                HiddenProductId.Value = product.ProductID.ToString();
                TbQuantity.Text = quantity.ToString();
            }
            else
            {
                Session["modal"] = null;
            }
        }

        protected void BtnAddToCart_Click(object sender, EventArgs e)
        {
            // Get quantity from input form 
            int quantityToBeAdded = int.Parse(TbQuantity.Text);

            // Get product id 
            int productId = int.Parse(HiddenProductId.Value);

            Session["modal"] = "show";

            Response.Redirect("~/ProductDetails.aspx?ProductId=" + productId
                + "&Quantity=" + quantityToBeAdded);
        }
    }
}