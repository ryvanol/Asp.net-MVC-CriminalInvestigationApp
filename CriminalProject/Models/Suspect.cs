using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalProject.Models
{
    [Table("Suspect")]
    public class Suspect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public ICollection<CrimeHistory> CommittedCrimes { get; set; }
        public Image SuspectPicture { get; set; }
        public int FkSuspectPictureId { get; set; }

        public Suspect()
        {
            CommittedCrimes= new HashSet<CrimeHistory>();
        }

    }
}