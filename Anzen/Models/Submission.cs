using System.ComponentModel.DataAnnotations;

namespace Anzen.Models
{
    public class Submission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccountName { get; set; } = null!;

        [Required]
        public string? UwName { get; set; } = null!;

        [Required]
        public decimal? Premium { get; set; } = null!;

        [Required]
        public DateOnly EffectiveDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required]
        public DateOnly ExpirationDate { get; set; } = DateOnly.FromDateTime(new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day)) ;

        [Required]
        public string? Sic { get; set; } = null!;
    }
}
