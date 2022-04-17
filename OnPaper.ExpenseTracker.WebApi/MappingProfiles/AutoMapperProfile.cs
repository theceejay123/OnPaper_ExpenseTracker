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
        CreateMap<Transaction, TransactionResponseDTO>()
            .ForMember(d => d.PaymentType, opts => opts.MapFrom(s => s.PaymentType.Name))
            .ForMember(d => d.Category, opts => opts.MapFrom(s => s.Category.Name))
            .ForMember(d => d.TransactionType, opts => opts.MapFrom(s => s.TransactionType.Name));
        CreateMap<Transaction, TransactionDTO>().ReverseMap();
    }
}