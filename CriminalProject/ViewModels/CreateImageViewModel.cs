using System.Collections.Generic;
using System.Web.Mvc;
using Image = CriminalProject.Models.Image;

namespace CriminalProject.ViewModels
{
    public class CreateImageViewModel
    {
        public Image Image { get; set; }
        public List<SelectListItem> Suspects { get; set; }
    }
}