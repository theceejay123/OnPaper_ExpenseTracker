using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.WebApi.DTOs;
using OnPaper.ExpenseTracker.WebApi.Errors;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[ProducesResponseType(StatusCodes.Status200OK)]
public class TransactionTypesController : BaseWebApiController
{
    private readonly ITransactionTypeService _transactionTypeService;
    private readonly IMapper _mapper;

    public TransactionTypesController(ITransactionTypeService transactionTypeService, IMapper mapper)
    {
        _transactionTypeService = transactionTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TransactionTypeDTO>>> ListTransactionTypes()
    {
        return Ok(_mapper.Map<IReadOnlyList<TransactionTypeDTO>>(await _transactionTypeService.ListTransactionTypesAsync()));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<TransactionTypeDTO>> GetTransactionType(int id)
    {
        var transactionType = await _transactionTypeService.GetTransactionTypeByIdAsync(id);
        if (transactionType == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound));
        }
        return Ok( _mapper.Map<TransactionTypeDTO>(transactionType));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TransactionType>> CreateTransactionType(TransactionTypeDTO transactionTypeDto)
    {
        var transactionType = await _transactionTypeService.CreateTransactionTypeAsync(_mapper.Map<TransactionType>(transactionTypeDto));
        if (transactionType == null)
        {
            return BadRequest(new ErrorException((int)HttpStatusCode.BadRequest, "Problem creating transaction type"));
        }
        return Ok(transactionType);
    }
}