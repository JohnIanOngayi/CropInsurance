using cropinsurance.server.Repository;
using cropinsurance.server.Services;
using Scalar.AspNetCore;

namespace darkdelta.server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.ConfigureCors();
            builder.Services.AddSingleton<DbContext>();
            builder.Services.ConfigureRepositoryWrapper();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
