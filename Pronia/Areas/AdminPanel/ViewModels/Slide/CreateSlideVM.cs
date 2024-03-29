﻿using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.AdminPanel.ViewModels
{
    public class CreateSlideVM
    {

        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
        [Required]

        public IFormFile Photo { get; set; }
    }
}
