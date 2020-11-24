using AutoMapper;
using FluentValidation;
using KCTest.Application.Services;
using KCTest.Domain;
using KCTest.Domain.DTOs;
using KCTest.Domain.Services;
using KCTest.Infrastructure;
using KCTest.Infrastructure.Database;
using KCTest.Infrastructure.Mapper;
using KCTest.Infrastructure.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KCTest.API
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

            services.AddDbContext<KCTestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("KCTestContext")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfileToDTOs());
                cfg.AddProfile(new MappingProfileToEntities());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPermissionTypeService, PermissionTypeService>();

            services.AddSingleton<IValidator<PermissionDto>, PermissionValidator>();
            services.AddSingleton<IValidator<PermissionTypeDto>, PermissionTypeValidator>();

            var client = Configuration.GetSection("ClientHost").Value;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                   builder => builder
                    .WithOrigins(Configuration.GetSection("ClientHost").Value)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                  );
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KCTest.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KCTest.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}