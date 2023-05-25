using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppinCartApp.DataAccess.Models
{
    [Table("Cart")]
    public class Cart
    {
        /// <summary>
        /// Foodcart entity
        /// </summary>
        [Key]
        [Column("Id")]
        public int CartId { get; set; }    //auto generated
        [Required]
        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        [DataType("float")]
        [Column("Price")]
        public float CartPrice { get; set; }
    }
}
