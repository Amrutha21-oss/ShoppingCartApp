using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.DataAccess.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Column("Id")]
        public int ProductId { get; set; }

        [Required]
        [DataType("varchar")]
        [Column("Name")]
        public string ProductName { get; set; }

        [MaxLength(50)]
        [DataType("varchar")]
        [Column("Description")]
        public string ProductDescription { get; set; }

        [MaxLength(100)]
        [DataType("float")]
        [Column("Cost")]
        public float ProductPrice { get; set; }

        [DataType("int")]
        [Column("Productcount")]
        public int ProductCount { get; set; }
    }
}
