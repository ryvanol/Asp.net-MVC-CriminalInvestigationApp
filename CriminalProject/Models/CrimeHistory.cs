using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalProject.Models
{
    [Table("CrimeHistory")]
    public class CrimeHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FkCrimeTypeId { get; set; }
        public CrimeType CrimeType { get; set; }
        public int FkWeaponId { get; set; }
        public Weapon Weapon { get; set; }
        public ApplicationUser Officer { get; set; }
        public string FkOfficerId { get; set; }
        public string Area { get; set; }
        public City City { get; set; }
        public int FkCityId { get; set; }
        public ICollection<Suspect> Suspects { get; set; }
        public string ResourceUrl { get; set; }

        public CrimeHistory()
        {
            Suspects = new HashSet<Suspect>();
        }
    }
}