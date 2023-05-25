using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.DataAccess.Models
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key]
        [Column("Id")]
        public int ProductCategoryId { get; set; }


        [ForeignKey("Categories")]
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        [ForeignKey("Products")]
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}
