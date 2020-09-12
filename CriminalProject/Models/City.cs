using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalProject.Models
{
    [Table("City")]
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CrimeHistory> Crimes { get; set; }

        public City()
        {
            Crimes = new HashSet<CrimeHistory>();
        }
    }
}