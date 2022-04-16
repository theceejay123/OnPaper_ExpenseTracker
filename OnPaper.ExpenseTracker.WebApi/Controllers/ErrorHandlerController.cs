using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.WebApi.Errors;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[Route("errors/{statusId}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorHandlerController : BaseWebApiController
{
    public IActionResult ErrorHandler(int statusId)
    {
        return new ObjectResult(new ErrorResponse(statusId));
    }
}