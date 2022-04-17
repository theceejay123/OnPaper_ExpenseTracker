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
public class CategoriesController : BaseWebApiController
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDTO>>> ListCategoriesAsync()
    {
        return Ok(_mapper.Map<IReadOnlyList<CategoryDTO>>(await _categoryService.ListCategoriesAsync()));
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound(new ErrorResponse((int)HttpStatusCode.NotFound));
        }

        return Ok(_mapper.Map<CategoryDTO>(category));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Category>> CreateCategory(CategoryDTO categoryDto)
    {
        var category = await _categoryService.CreateCategoryAsync(_mapper.Map<Category>(categoryDto));
        if (category == null)
        {
            return BadRequest(new ErrorException((int)HttpStatusCode.BadRequest, "Problem creating category"));
        }
        return Ok(category);
    }
}