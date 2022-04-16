using System.Net;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.WebApi.DTOs;
using OnPaper.ExpenseTracker.WebApi.Errors;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

[ProducesResponseType(StatusCodes.Status200OK)]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class PaymentTypesController : BaseWebApiController
{
    private readonly IPaymentTypeService _paymentTypeService;
    private readonly IMapper _mapper;

    public PaymentTypesController(IPaymentTypeService paymentTypeService, IMapper mapper)
    {
        _paymentTypeService = paymentTypeService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PaymentTypeDTO>>> ListPaymentTypes()
    {
        return Ok(_mapper.Map<IReadOnlyList<PaymentTypeDTO>>(await _paymentTypeService.ListPaymentTypesAsync()));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<PaymentTypeDTO>> GetPaymentType(int id)
    {
        var paymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(id);
        if (paymentType == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound));
        }
        return Ok( _mapper.Map<PaymentTypeDTO>(paymentType));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PaymentType>> CreatePaymentType(PaymentTypeDTO PaymentTypeDto)
    {
        var paymentType = await _paymentTypeService.CreatePaymentTypeAsync(_mapper.Map<PaymentType>(PaymentTypeDto));
        if (paymentType == null)
        {
            return BadRequest(new ErrorException((int)HttpStatusCode.BadRequest, "Problem creating payment type"));
        }
        return Ok(paymentType);
    }
}