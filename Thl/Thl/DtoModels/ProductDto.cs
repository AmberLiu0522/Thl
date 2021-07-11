using System.ComponentModel.DataAnnotations;

namespace Thl.Models
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}