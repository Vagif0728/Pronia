using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Size
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is important")]
        [MaxLength(10, ErrorMessage = "Max length is 10")]
        public string Name { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
    }
}
