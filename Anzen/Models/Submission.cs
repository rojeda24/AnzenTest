using System.ComponentModel.DataAnnotations;

namespace Anzen.Models
{
    public class Submission
    {
        [Key]
        public int Id {get; set; }
        [Required]
        public string AccountName { get; set; } = null!;
    }
}
