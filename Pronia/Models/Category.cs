using Pronia.Models;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is important")]
        [MaxLength(25, ErrorMessage = "Length not is more 25")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
