using System.ComponentModel.DataAnnotations;

namespace OnPaper.ExpenseTracker.Core.Models.Identity;

public class Address : BaseModel
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }

    [Required] public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}