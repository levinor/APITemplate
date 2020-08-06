using AutoMapper;
using Levinor.APITemplate.Configuration;
using Levinor.APITemplate.Mapping;
using Levinor.Business.EF.SQL;
using Levinor.Business.Repositories;
using Levinor.Business.Repositories.Interfaces;
using Levinor.Business.Services;
using Levinor.Business.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            services
                .AddMvcCore(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            services.AddMemoryCache();

            //Adding Swagger
            services.AddSwaggerGen(c =>
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "APITemplate", Version = "v1" })
            ); 

            //Registering the SQL Entity Framework Context
            services.AddDbContext<SQLEFContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("SQLDatabase")));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<ISQLRepository, SQLRepository>();

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICacheService, CacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {                   
            app.UseRouting();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TemplateApi V1");
                });

            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvc();


        }
    }
}
