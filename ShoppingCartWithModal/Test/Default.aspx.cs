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
    public partial class _Default : Page
    {
        public string modal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int productId = 1;

                if (Session["modal"] == null)
                    this.modal = "hide";
                else
                    this.modal = Session["modal"].ToString();

                if (Request.QueryString["ProductId"] == null)
                {
                    // Not come from the product list page
                    productId = 2;
                }
                else
                    // Get product id
                    productId = int.Parse(Request.QueryString["ProductId"] ?? "0");

                ctlCart.ProductID = productId;

                ctlCart.Quantity = 1;

                productRepeater.DataSource = Product.Select();
                productRepeater.DataBind();
            }
            else
                Session["modal"] = null;
        }

        protected void BtnAddToCart_Click(object sender, EventArgs e)
        {
            int productID = 0;
            int.TryParse(((Button)sender).CommandArgument, out productID);
            ctlCart.ProductID = productID;

            Session["modal"] = "show";

            Response.Redirect("~/Default.aspx?ProductId=" + productID);
        }
    }
}