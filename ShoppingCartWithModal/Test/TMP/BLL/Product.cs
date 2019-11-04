using Dapper;
using SShoppingCart.TMP.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SShoppingCart.TMP.BLL
{
    public class Product
    {
        public static ProductEntity Get(int productID)
        {
            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = "select * from products where ProductID={0};";
                sql = string.Format(sql, productID);
                return db.QueryFirstOrDefault<ProductEntity>(sql);
            }
        }

        public static List<ProductEntity> Select()
        {
            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = "select * from products";
                return db.Query<ProductEntity>(sql).ToList();
            }
        }

        public static List<ProductEntity> SelectByCardId(string cardItem)
        {
            using (var db = DatabaseManager.GetOpenConnection())
            {
                var sql = @"select p.* from products p
                            inner join cartitems c on c.productid=p.productid
                            where c.cartid='{0}';";
                sql = string.Format(sql, cardItem);
                return db.Query<ProductEntity>(sql).ToList();
            }
        }
    }
}