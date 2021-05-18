using Api.Accounts.Domain;
using Api.Accounts.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: new OpenApiInfo
                    {
                        Title = "API",
                        Version = "v1"
                    });
            });

            services.AddControllers();

            services.AddSingleton<IAccountRepository, MemoryAccountRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));
        }
    }
}
