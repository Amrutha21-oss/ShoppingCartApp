using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.DataAccess.Models
{
    [Table("Orders")]
    public class Order
    {
        /// <summary>
        /// Order entity
        /// </summary>
        [Key]
        [Column("Id")]
        public int OrderId { get; set; }
        [ForeignKey("Cart")]
        public int? CartId { get; set; }     //adding foreign key from FoodCart table //make sure it's non nullable
        public virtual Cart? Cart { get; set; }
        [ForeignKey("Addresses")]
        public int? AddressId { get; set; }       //adding foreign key from address table
        public virtual Address? Address { get; set; }
        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public virtual User? User { get; set; }   //adding foreign key from User table
        [DataType("float")]
        [Column("Amount")]
        public float OrderPrice { get; set; }
        [DataType("varchar")]
        [Column("Status")]
        public string OrderStatus { get; set; }
    }
}
