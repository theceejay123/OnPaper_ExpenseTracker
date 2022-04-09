using System.ComponentModel.DataAnnotations;

namespace OnPaper.ExpenseTracker.Core.Models.Identity;

public class Address : BaseModel
{
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    [Required] public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;
}