using System.Linq;
using ContaCorrente.ApiExtrato.Data;
using ContaCorrente.ApiExtrato.Models;
using ContaCorrente.ApiExtrato.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ContaCorrente.ApiExtrato
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();

            services.Configure<TransacoesDatabaseSettings>(
                Configuration.GetSection(nameof(TransacoesDatabaseSettings)));

            services.AddSingleton<ITransacoesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TransacoesDatabaseSettings>>().Value);

            services.AddSingleton<TransacoesDataContext>();

            services.AddSingleton<ProcessTransacao>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ContaCorrente.ApiExtrato",
                    Version = "v1"
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContaCorrente.ApiExtrato v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var bus = app.ApplicationServices.GetService<ProcessTransacao>();
            bus.RegisterHandler();
        }
    }
}
