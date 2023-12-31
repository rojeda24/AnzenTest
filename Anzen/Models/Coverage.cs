using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Anzen.Models
{
    public class Coverage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public List<Submission> Submissions { get; } = new();

    }
}
