
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
                    options.DocumentTitle = "BugTracker BackEnd API";
                    options.ConfigObject.DefaultModelExpandDepth = -1;
                    options.ConfigObject.ShowExtensions = false;
                    options.ConfigObject.TryItOutEnabled = false;
                    options.ConfigObject.ShowCommonExtensions = false;
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