#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class Wedding
{
    [Key]
    public int WeddingId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "First name must be at least 2 characters!")]
    [DisplayName("Wedder One:")]
    public string WedderOne { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 characters!")]
    [DisplayName("Wedder Two:")]
    public string WedderTwo { get; set; }

    [Required]
    [DisplayName("Date")]
    [FutureDate]
    public DateTime TheBigDay { get; set; }

    [Required]
    public string Address { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; } = DateTime.Now;

    //fk
    public int UserId { get; set; }

    // NAV PROP
    public User? Creator { get; set; }

    public List<Decision> UserDecision { get; set; } = new();

}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || (DateTime)value < DateTime.Now)
        {
            return new ValidationResult("Birthday must be in the future");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}