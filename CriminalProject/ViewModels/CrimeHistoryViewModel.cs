using System.Collections.Generic;
using System.Web.Mvc;
using CriminalProject.Models;

namespace CriminalProject.ViewModels
{
    public class CrimeHistoryViewModel
    {
        public CrimeHistory CrimeHistory { get; set; }
        public List<SelectListItem> Officers { get; set; }
        public List<SelectListItem> Weapons { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> CrimeTypes { get; set; }

    }
}