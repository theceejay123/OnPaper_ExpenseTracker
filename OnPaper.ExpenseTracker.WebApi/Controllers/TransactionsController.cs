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

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[Produces(MediaTypeNames.Application.Json)]
public class TransactionsController : BaseWebApiController
{
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public TransactionsController(ITransactionService transactionService, IMapper mapper)
    {
        _transactionService = transactionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TransactionResponseDTO>>> GetTransactions([FromQuery][ModelBinder(typeof(HashidsModelBinder))] int workBookId)
    {
        var result = await _transactionService.ListTransactionsAsync(workBookId);
        return Ok(_mapper.Map<IReadOnlyList<TransactionResponseDTO>>(result));
    }

    [HttpGet]
    [Route("{id:hashids}")]
    public async Task<ActionResult<TransactionResponseDTO>> GetTransaction([FromRoute][ModelBinder(typeof(HashidsModelBinder))] int id, [FromQuery][ModelBinder(typeof(HashidsModelBinder))] int workBookId)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id, workBookId);
        if (transaction == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "The request transaction was not found"));
        }

        return Ok(_mapper.Map<TransactionResponseDTO>(transaction));
    }

    [HttpPost]
    public async Task<ActionResult<TransactionResponseDTO>> CreateTransaction(TransactionDTO transactionDto)
    {
        var result = await _transactionService.CreateTransactionAsync(_mapper.Map<TransactionDTO, Transaction>(transactionDto));
        if (result == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "Problem creating transaction. No workbook found"));
        }
        return Ok(_mapper.Map<Transaction, TransactionResponseDTO>(result));
    }
    
    [HttpPatch]
    public async Task<ActionResult<TransactionResponseDTO>> UpdateTransaction(TransactionDTO transactionDto)
    {
        var result = await _transactionService.UpdateTransactionAsync(_mapper.Map<TransactionDTO, Transaction>(transactionDto));
        if (result == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "Problem updating transaction. No workbook found"));
        }
        return Ok(_mapper.Map<TransactionResponseDTO>(result));
    }
    
    [HttpDelete]
    [Route("{id:hashids}")]
    public async Task<ActionResult<TransactionResponseDTO>> DeleteTransaction([FromRoute] [ModelBinder(typeof(HashidsModelBinder))] int id, [FromQuery][ModelBinder(typeof(HashidsModelBinder))] int workBookId)
    {
        var result = await _transactionService.DeleteTransactionAsync(id, workBookId);
        if (result == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound, "Cannot find transaction to delete"));
        }

        return Ok(_mapper.Map<TransactionResponseDTO>(result));
    }
}