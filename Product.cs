using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMS.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "it cannot be empty")]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "it cannot be empty")]
        public int ReorderLevel { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

    }
}
