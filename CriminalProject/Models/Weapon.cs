using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalProject.Models
{
    [Table("Weapon")]
    public class Weapon
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<CrimeHistory> Crimes { get; set; }

        public Weapon()
        {
            Crimes = new HashSet<CrimeHistory>();
        }
    }
}