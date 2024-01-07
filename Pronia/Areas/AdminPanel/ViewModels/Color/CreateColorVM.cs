using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class CreateColorVM
    {
        [MaxLength(10, ErrorMessage = "Max length is 10")]
        public string Name { get; set; }
    }
}
