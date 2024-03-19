
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Market.Abstraction;
using Market.Models;
using Market.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Market
{
    public class Program
    {
        public static WebApplication AppBuilding(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductGroupRepository, ProducGroupRepository>();

            builder.Services.AddMemoryCache(mc => mc.TrackStatistics = true);

            string? connectionString = builder.Configuration.GetConnectionString("db");
            builder.Services.AddDbContext<ProductContext>(o => o.UseSqlServer(connectionString));

            return builder.Build();
        }

            public static void Main(string[] args)
            {
                var app = AppBuilding(args);

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                var staticFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");
                Directory.CreateDirectory(staticFilesPath);

                app.UseStaticFiles(
                    new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(staticFilesPath),
                        RequestPath = "/static"
                    });

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
        }
    }
