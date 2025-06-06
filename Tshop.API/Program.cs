
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Tshop.API.Data;
using Tshop.API.Services;


namespace Tshop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
           builder.Services.AddScoped<ICategoryService,CategoryService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //app.UseSwagger();
                //app.UseSwaggerUI();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //var context = new ApplicationDbContext();

            //try
            //{
            //    context.Database.CanConnect();
            //    Console.WriteLine("done");

            //}
            //catch (Exception ex) {
            //    Console.WriteLine("error");
            //}

            app.MapControllers();

            app.Run();
        }
    }
}
