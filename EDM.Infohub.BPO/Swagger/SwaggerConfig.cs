using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;


namespace EDM.Infohub.BPO.Swagger
{
    public static class SwaggerConfig
    {
        public static IServiceCollection SetupSwaggerDoc(this IServiceCollection services, string environment)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BPO " + environment, Version = "v1" });
                option.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BPO " + environment, Version = "v2" });
            });
            return services;
        }

        public static void ConfigureSwaggerUI(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, string environment)
        {
            app.UseSwagger();
            app.UseSwaggerUI(param =>
            {
                param.RoutePrefix = "swagger";
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    param.DocumentTitle = "EDM.BPO Docs " + environment;
                    param.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", "BPO " + description.GroupName.ToUpperInvariant());
                }
                param.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);

            });
        }
    }
}
