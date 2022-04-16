using AutoMapper;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.WebApi.DTOs;

namespace OnPaper.ExpenseTracker.WebApi.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TransactionType, TransactionTypeDTO>().ReverseMap();
        CreateMap<PaymentType, PaymentTypeDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<WorkBook, WorkBookDTO>().ReverseMap();
    }
}