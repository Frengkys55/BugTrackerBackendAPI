
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS
            string corsPolicyName = "_allowAllOrigins";
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: corsPolicyName,
                    policy =>
                    {
                        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                    });
            });


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