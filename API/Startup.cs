using API.Context;
using API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Linq;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    services.AddControllers();
        //    services.AddScoped<OrdersRepository>();
        //    services.AddScoped<ItemsRepository>();



        //    services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("pb")));
        //    services.AddCors(c =>
        //    {
        //        c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
        //    });

        //}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure Dapper and IDbConnection
            services.AddScoped<IDbConnection>(x =>
            {
                var configuration = x.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("pb");
                return new SqlConnection(connectionString);
            });

            // Configure ItemsRepository with dependency injection
            services.AddScoped<ItemsRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("pb");
                return new ItemsRepository(connectionString);
            });

            services.AddScoped<OrdersRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("pb");
                return new OrdersRepository(connectionString);
            });

            // Configure CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();



            app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());

            //app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
