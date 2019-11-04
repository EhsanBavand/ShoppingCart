using SShoppingCart.TMP.DAL;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SShoppingCart.TMP.DAL
{

    [Table("dbo.CartItems")]
    public class CartItemEntity
    {
        public int ItemId { get; set; }

        public string CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ProductEntity Product { get; set; }

        public double Total
        {
            get
            {
                double price = Product.UnitPrice ?? 0;

                return price * Quantity;
            }

        }

        public static implicit operator CartItemEntity(char v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator CartItemEntity(string v)
        {
            throw new NotImplementedException();
        }
    }
}