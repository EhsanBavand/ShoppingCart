using Dapper;
using SShoppingCart.TMP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SShoppingCart.TMP.BLL
{
    public class CartItem
    {
        // Variables declaration
        public string ShoppingCartId { get; set; }
        public object ItemId { get; private set; }

        public const string CartCookieKey = "CartId";

        /// <summary>
        /// Add products to Cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public CartItemEntity AddToCart(int productId, int quantity)
        {
            // Retrieve shopping cart id         
            ShoppingCartId = CartCookieKey;

            CartItemEntity cartItem = null;

            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = "select * from cartitems where CartId='{0}' AND ProductId={1};";
                sql = string.Format(sql, ShoppingCartId, productId);
                cartItem = db.QuerySingleOrDefault<CartItemEntity>(sql);
            }

            // The product does not exist in the cart
            if (cartItem == null)
            {
                // Create a new cart item when the product does not exist in the db                
                cartItem = new CartItemEntity
                {
                    CartId = ShoppingCartId,
                    ProductId = productId,
                    Quantity = quantity,
                    DateCreated = DateTime.Now
                };
            }
            else
            {
                // If the item does exist in the cart, add quantity.                 
                cartItem.Quantity += quantity;
            }

            if (cartItem != null)
            {
                cartItem.Product = Product.Get(productId);
            }

            // Update Cart item to DB   
            SaveChanges(cartItem);

            return cartItem;
        }

        public bool SaveChanges(CartItemEntity cart)
        {
            bool result = false;
            try
            {
                using (var db = DatabaseManager.GetOpenConnection())
                {
                    var sql = "";
                    if (cart.ItemId == 0)
                    {
                        sql = "INSERT INTO CartItems (CartId, ProductId , Quantity, DateCreated) VALUES ('{0}', {1}, {2}, '{3}');";
                        sql = string.Format(sql, cart.CartId, cart.ProductId, cart.Quantity, cart.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        sql = "UPDATE CartItems SET CartId='{0}', ProductId={1}, Quantity={2} WHERE ItemId={3};";
                        sql = string.Format(sql, cart.CartId, cart.ProductId, cart.Quantity, cart.ItemId);
                    }

                    result = db.Execute(sql) > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        /// <summary>
        /// Get all items from cart by Cart Id
        /// </summary>
        /// <returns></returns>
        public List<CartItemEntity> GetCartItems()
        {
            ShoppingCartId = CartCookieKey;

            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = "select * from cartitems where CartId='{0}';";
                sql = string.Format(sql, ShoppingCartId);
                var entities = db.Query<CartItemEntity>(sql).ToList();
                if (entities.Count() > 0)
                {
                    var products = Product.SelectByCardId(ShoppingCartId);
                    entities.ForEach(e => e.Product = products.FirstOrDefault(p => p.ProductID == e.ProductId));
                }

                return entities;
            }
        }

        /// <summary>
        /// Get total of all cart items
        /// </summary>
        /// <returns></returns>
        public double GetTotal()
        {
            double? total = 0;
            var items = GetCartItems();
            total = (double?)(from cartitem in items
                              select (int?)cartitem.Quantity * cartitem.Product.UnitPrice).Sum();
            return total ?? 0;
        }

        /// <summary>
        /// Get count of all cart items
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up          
            int? count = (from cartItem in GetCartItems()
                          select (int?)cartItem.Quantity).Sum();
            // Return 0 if all entries are null         
            return count ?? 0;
        }

        /// <summary>
        /// Update quantity in shopping cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>

        public void UpdateCart(int productId, int quantity)
        {
            ShoppingCartId = CartCookieKey;

            using (var db = DatabaseManager.GetOpenConnection())
            {
                string sql = string.Empty;

                // Update the quantitiy to new value
                if (quantity > 0)
                {
                    sql = "UPDATE CartItems SET Quantity={0} WHERE CartId='{1}' and productid={2};";
                    sql = string.Format(sql, quantity, ShoppingCartId, productId);
                }
                // Delete the item if the quantity was set to 0
                else
                {
                    sql = "delete from CartItems WHERE CartId='{0}' and productid={1};";
                    sql = string.Format(sql, ShoppingCartId, productId);
                }

                db.Execute(sql);
            }

        }

        /// <summary>
        /// Empty cart after payment
        /// </summary>
        public void EmptyCart()
        {
            ShoppingCartId = CartCookieKey;

            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = "delete from dbo.cartitems where CartId='{0}';";
                sql = string.Format(sql, ShoppingCartId);
                db.Execute(sql);
            }
        }

        public List<CartItemEntity> Select(int productId)
        {
            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = @"select c.* from CartItems as c 
                            inner join Products as p on c.ProductId = p.ProductId 
                            where p.ProductId ='{0}' and c.CartID ='{1}';";
                sql = string.Format(sql, productId, CartCookieKey);

                var items = db.Query<CartItemEntity>(sql).ToList();
                var product = Product.Get(productId);
                if (product != null)
                {
                    items.ForEach(i => i.Product = product);
                }

                return items;
            }
        }
    }

}
