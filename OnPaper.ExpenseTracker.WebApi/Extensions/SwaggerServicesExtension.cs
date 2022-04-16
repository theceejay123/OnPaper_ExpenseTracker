using Microsoft.OpenApi.Models;
using OnPaper.ExpenseTracker.WebApi.Helpers;

namespace OnPaper.ExpenseTracker.WebApi.Extensions;

public static class SwaggerServicesExtension
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(gen =>
        {
            gen.OperationFilter<HashIdsFilters>();
            gen.SwaggerDoc("v1", new OpenApiInfo() {Title = "WebAPIv6", Version = "v1"});
            var securityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Jwt Auth Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Reference = new OpenApiReference() {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
            };
            
            gen.AddSecurityDefinition("Bearer", securityScheme);
            var securityRequirements = new OpenApiSecurityRequirement() {{securityScheme, new[] {"Bearer"}}};
            gen.AddSecurityRequirement(securityRequirements);
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(doc => doc.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv6 v1"));
        return app;
    }
}