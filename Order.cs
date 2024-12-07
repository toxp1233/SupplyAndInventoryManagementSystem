using ISMS.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid amount ordered")]
    public int QuantityOrdered { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }
    public virtual Product Product { get; set; }
}
