using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppinCartApp.DataAccess.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Column("Id")]
        public int CategoryId { get; set; }

        [Required]
        [DataType("varchar")]
        [Column("Name")]
        public string CategoryName { get; set; }
    }
}
