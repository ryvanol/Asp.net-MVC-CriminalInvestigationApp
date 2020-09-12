using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalProject.Models
{
    [Table("Image")]
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Suspect Suspect { get; set; }
        public int FkSuspectId { get; set; }
    }
}