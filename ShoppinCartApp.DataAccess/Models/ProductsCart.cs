using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppinCartApp.DataAccess.Models
{
    [Table("ProductsCart")]
    public class ProductsCart
    {
        /// <summary>
        /// ProductsCart entity
        /// This is a mapping table for tables products and cart
        /// </summary>
        [Key]
        [Column("Id")]
        public int ProductsCartId { get; set; }    //auto generated
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public virtual Cart? Cart { get; set; }
        [DataType("int")]
        [Column("Count")]
        public int CartCount { get; set; }
    }
}
