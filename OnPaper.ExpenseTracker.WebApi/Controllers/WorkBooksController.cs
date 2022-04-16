using System.Net;
using System.Net.Mime;
using AspNetCore.Hashids.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.WebApi.DTOs;
using OnPaper.ExpenseTracker.WebApi.Errors;
using OnPaper.ExpenseTracker.WebApi.Extensions;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class WorkBooksController : BaseWebApiController
{
    private readonly IWorkBookService _workBookService;
    private readonly IMapper _mapper;

    public WorkBooksController(IWorkBookService workBookService, IMapper mapper)
    {
        _workBookService = workBookService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkBookDTO>>> GetWorkBooks()
    {
        var appUserId = HttpContext.User.GetUserIdFromClaimsPrincipal();
        var result = await _workBookService.ListWorkBooksAsync(appUserId);
        return Ok(_mapper.Map<IReadOnlyList<WorkBookDTO>>(result));
    }

    [HttpGet]
    [Route("{id:hashids}")]
    public async Task<ActionResult<WorkBookDTO>> GetWorkBook([FromRoute][ModelBinder(typeof(HashidsModelBinder))] int id)
    {
        var appUserId = HttpContext.User.GetUserIdFromClaimsPrincipal();
        var workBook = await _workBookService.GetWorkBookByIdAsync(id, appUserId);
        if (workBook == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "The requested workbook was not found"));
        }
        
        return Ok(_mapper.Map<WorkBookDTO>(workBook));
    }

    [HttpPost]
    public async Task<ActionResult<WorkBookDTO>> CreateWorkBook(WorkBookDTO workBookDTO)
    {
        var workBook = _mapper.Map<WorkBook>(workBookDTO);
        workBook.AppUserId = HttpContext.User.GetUserIdFromClaimsPrincipal();
        var result = await _workBookService.CreateWorkBookAsync(workBook);
        if (result == null)
        {
            return BadRequest(new ErrorResponse((int)HttpStatusCode.BadRequest, "Problem creating workbook"));
        }
        
        return Ok(_mapper.Map<WorkBookDTO>(result));
    }

    [HttpPatch]
    public async Task<ActionResult<WorkBookDTO>> UpdateWorkBook(WorkBookDTO workBookDto)
    {
        var workBookToUpdate = _mapper.Map<WorkBook>(workBookDto);
        workBookToUpdate.AppUserId = HttpContext.User.GetUserIdFromClaimsPrincipal();
        var result = await _workBookService.UpdateWorkBookAsync(workBookToUpdate);

        if (result == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "Problem updating workbook. No workbook found"));
        }
        return Ok(_mapper.Map<WorkBookDTO>(result));
    }

    [HttpDelete]
    [Route("{id:hashids}")]
    public async Task<ActionResult<WorkBookDTO>> DeleteWorkBook([FromRoute] [ModelBinder(typeof(HashidsModelBinder))] int id)
    {
        var appUserId = HttpContext.User.GetUserIdFromClaimsPrincipal();
        var result = await _workBookService.DeleteWorkBookAsync(id, appUserId);
        if (result == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "Cannot find workbook to delete"));
        }

        return Ok(_mapper.Map<WorkBookDTO>(result));
    }
}