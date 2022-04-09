using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.WebApi.Errors;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
public class WorkBooksController : BaseWebApiController
{
    private readonly IGenericRepository<WorkBook> _workBooksRepository;

    public WorkBooksController(IGenericRepository<WorkBook> workBooksRepository)
    {
        _workBooksRepository = workBooksRepository;
    }

    [HttpGet]
    public async Task<ActionResult<WorkBook>> GetWorkBooks()
    {
        return Ok(await _workBooksRepository.ListAsync());
    }
}