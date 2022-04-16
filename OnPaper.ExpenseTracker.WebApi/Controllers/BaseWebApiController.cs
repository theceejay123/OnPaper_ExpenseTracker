using Microsoft.AspNetCore.Mvc;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseWebApiController : ControllerBase
{
}