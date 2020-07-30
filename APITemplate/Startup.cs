using Levinor.APITemplate.Configuration;
using Levinor.Business.EF.SQL;
using Levinor.Business.Repositories;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services;
using Levinor.Business.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace APITemplate
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
            services.AddControllers();
            services.AddApiVersioning();

            //Adding Swagger
            services.AddSwaggerGen(c =>
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "APITemplate", Version = "v1" })
            ); 

            //Registering the SQL Entity Framework Context
            services.AddDbContext<SQLEFContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("SQLDatabase")));

            services.AddSingleton<ISQLRepository, SQLRepository>();

            services.AddSingleton<IUserService, UserService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TemplateApi V1");
                });

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
