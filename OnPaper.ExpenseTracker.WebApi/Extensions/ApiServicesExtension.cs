using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Infrastructure.Data;
using OnPaper.ExpenseTracker.Infrastructure.Services;
using OnPaper.ExpenseTracker.WebApi.Errors;

namespace OnPaper.ExpenseTracker.WebApi.Extensions;

public static class ApiServicesExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IGenerateTokenKeyService>(_ => new GenerateTokenKeyService());
        services.AddScoped<IWorkBookService, WorkBookService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPaymentTypeService, PaymentTypeService>();
        services.AddScoped<ITransactionTypeService, TransactionTypeService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(k => k.ErrorMessage).ToArray();
                var response = new ErrorValidation
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(response);
            };
        });

        return services;
    }
}