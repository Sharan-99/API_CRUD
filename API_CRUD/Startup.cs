using API_CRUD.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //for enabling CORS Policy - Nuget Package to install: Microsoft.AspNetCore.Cors
            //should always be added as the first service in Configure Services 
            services.AddCors();

            services.AddControllers();

            var conn = Configuration.GetConnectionString("constr");
            services.AddDbContext<APICRUD_DBContext>(setup => setup.UseSqlServer(conn));
            services.AddScoped<IRepository<Product>, GenericRepository<Product>>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_CRUD", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_CRUD v1"));
            }

            app.UseRouting();

            //for enabling CORS policy, should always be added after UseRouting Middleware
            app.UseCors(setup => setup.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // for public API's 

            //app.UseCors(setup => setup.WithOrigins("http://abc.com", "http://google.com")    //for allowing only particular origins to access the API
            //                           .WithMethods("POST","GET")
            //                           .WithHeaders("Accept","Content-type"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
