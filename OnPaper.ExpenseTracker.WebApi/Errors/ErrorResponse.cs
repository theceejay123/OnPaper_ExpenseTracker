namespace OnPaper.ExpenseTracker.WebApi.Errors;

public class ErrorResponse
{
    public ErrorResponse(int statusId, string message = null)
    {
        StatusId = statusId;
        Code = GetStatusCode(statusId);
        Message = message ?? GetDefaultErrorMessage(statusId);
    }

    public int StatusId { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }

    private string GetStatusCode(int statusId)
    {
        return statusId switch
        {
            400 => "BAD_REQUEST",
            401 => "ACCESS_UNAUTHORIZED",
            403 => "ACCESS_FORBIDDEN",
            404 => "NOT_FOUND",
            500 => "INTERNAL_SERVER_ERROR",
            502 => "BAD_GATEWAY",
            503 => "SERVICE_UNAVAILABLE",
            504 => "TIMED_OUT",
            501 => "NOT_IMPLEMENTED",
            _ => null
        };
    }

    private string GetDefaultErrorMessage(int statusId)
    {
        return statusId switch
        {
            400 => "A bad request has been made.",
            401 => "Access is not authorized for this request.",
            403 => "Access is forbidden for this request",
            404 => "Request item has not been found.",
            500 => "Server error. Please try again later.",
            502 => "Server request denied. Please try again later.",
            503 => "Server unavailable at this moment. Please try again later",
            504 => "Server timed-out. The request has been cancelled.",
            501 => "The following request has not yet been implemented.",
            _ => null
        };
    }
}