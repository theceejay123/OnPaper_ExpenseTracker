namespace OnPaper.ExpenseTracker.WebApi.Errors;

public class ErrorException : ErrorResponse
{
    public ErrorException(int statusId, string message = null, string stackTrace = null) : base(statusId, message)
    {
        StackTrace = stackTrace;
    }

    public string StackTrace { get; set; }
}