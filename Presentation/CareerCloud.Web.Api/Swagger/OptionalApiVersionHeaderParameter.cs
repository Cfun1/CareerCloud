using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CareerCloud.WebAPI.Swagger;

public class OptionalApiVersionHeaderParameter : IOperationFilter
{
    string _apiVersionHeader;
    public OptionalApiVersionHeaderParameter(string apiVersionHeader)
    {
        _apiVersionHeader = apiVersionHeader;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            return;

        var versionParameter = operation.Parameters
            .FirstOrDefault(p => p.Name == _apiVersionHeader);

        if (versionParameter != null)
        {
            versionParameter.Required = false;
            // versionParameter.Content = new OpenApiString(context.ApiDescription.GroupName);
            versionParameter.Example = new OpenApiString(context.ApiDescription.GroupName.Replace("v", ""));
        }
    }
}