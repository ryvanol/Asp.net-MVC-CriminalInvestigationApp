using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalProject.Models
{
    [Table("CrimeType")]
    public class CrimeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CrimeHistory> Crimes { get; set; }

        public CrimeType()
        {
            Crimes = new HashSet<CrimeHistory>();
        }
    }
}