using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CareerCloud.WebAPI.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IOptions<ApiVersioningOptions> _options;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<ApiVersioningOptions> options)
    {
        _provider = provider;
        _options = options;
    }
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

    }
    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = AppDomain.CurrentDomain.FriendlyName,
            Version = description.ApiVersion.ToString(),
        };

        if (description.ApiVersion == _options.Value.DefaultApiVersion)
        {
            if (!string.IsNullOrWhiteSpace(info.Description))
            {
                info.Description += " ";
            }

            info.Description += "Default API version.";
        }

        if (description.IsDeprecated)
        {
            if (!string.IsNullOrWhiteSpace(info.Description))
            {
                info.Description += " ";
            }

            info.Description += "This API version is deprecated.";
        }
        return info;
    }
}