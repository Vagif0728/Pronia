using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class CreateSizeVM
    {
        [MaxLength(10, ErrorMessage = "Max lenght is 10")]
        public string Name { get; set; }
    }
}
