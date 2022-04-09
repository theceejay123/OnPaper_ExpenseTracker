using System.Net;

namespace OnPaper.ExpenseTracker.WebApi.Errors;

public class ErrorValidation : ErrorResponse
{
    public ErrorValidation() : base((int)HttpStatusCode.BadRequest)
    {
    }
    
    public IEnumerable<string>? Errors { get; set; }
}