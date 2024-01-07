using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class CreateTagVM
    {
        [MaxLength(10, ErrorMessage = "Max uzunluq 10 olmalidir")]
        public string Name { get; set; }
    }
}
