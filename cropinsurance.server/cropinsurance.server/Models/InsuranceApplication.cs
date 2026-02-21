using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace cropinsurance.server.Models
{
    public class InsuranceApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int SeasonId { get; set; }

        public string? SeasonName { get; set; }

        [Required]
        public int CropId { get; set; }

        public string? CropName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "No special characters allowed")]
        public string FarmerName { get; set; } = null!;

        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhaar must be exactly 12 digits")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Numbers only")]
        public string AadNo { get; set; } = null!;

        [Required]
        [StringLength(12)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Alphanumeric only")]
        public string FatherName { get; set; } = null!;

        [Required]
        [StringLength(250)]
        public string CompleteAddress { get; set; } = null!;

        [Required]
        public string FarmerCategory { get; set; } = null!;

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
