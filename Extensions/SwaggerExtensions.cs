using System.Reflection;
using Microsoft.OpenApi.Models;

namespace LibrarySystem.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenWithXml(this IServiceCollection services, Assembly assembly)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "System biblioteczny API",
                    Version = "v1",
                    Description = "Backend ASP.NET Core Web API dla systemu bibliotecznego. Projekt finalny obejmuje etapy 1, 2 i 3."
                });

                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
