using AspNetCore.Hashids.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnPaper.ExpenseTracker.WebApi.Helpers;

public class HashIdsFilters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hashids = context
            .ApiDescription
            .ParameterDescriptions
            .Where(x => x.ModelMetadata.BinderType == typeof(HashidsModelBinder))
            .ToDictionary(d => d.Name, d => d);

        foreach (var parameter in operation.Parameters)
        {
            if (!hashids.TryGetValue(parameter.Name, out var apiParameter)) continue;
            parameter.Schema.Format = string.Empty;
            parameter.Schema.Type = "string";
        }

        foreach (var (key, value) in context.SchemaRepository.Schemas.Values.SelectMany(schema => schema.Properties))
        {
            if (!hashids.TryGetValue(key, out var apiParameter)) continue;
            value.Format = string.Empty;
            value.Type = "string";
        }
    }
}