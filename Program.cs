
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Services;
using System.Reflection.Metadata;

namespace BugTrackerBackendAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            // Configure CORS
            string corsPolicyName = "AllowAllOrigins";


            builder.Services.AddCors(o => o.AddPolicy("AllowAllOrigins",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  }));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "BugTracker Backend API";
                    //options.DefaultModelsExpandDepth(-1);
                    //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                    //options.SupportedSubmitMethods(new Swashbuckle.AspNetCore.SwaggerUI.SubmitMethod[0]);
                    options.EnableDeepLinking();
                });
            }

            app.UseHttpsRedirection();
            app.UseCors(corsPolicyName);
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}