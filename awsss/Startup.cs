using Amazon.Runtime;
using Amazon.S3;
using awsss.Domain.Services;
using awsss.Filters;
using awsss.Persistence;
using awsss.Services;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
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

namespace awsss
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.EnableSensitiveDataLogging(true);
                options.UseNpgsql(Configuration.GetConnectionString("default"), x =>
                {
                    x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });

            });
           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "awsss", Version = "v1" });
            });
            services.AddHangfire(configuration => configuration
              .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
              .UseSimpleAssemblyNameTypeSerializer()
              .UseRecommendedSerializerSettings()
              .UsePostgreSqlStorage(Configuration.GetConnectionString("default")
              , new PostgreSqlStorageOptions
              {
                  DistributedLockTimeout = TimeSpan.FromMinutes(1),

              }));
            services.AddHangfireServer();

            var options = Configuration.GetAWSOptions();
            try
            {
                options.Credentials = new EnvironmentVariablesAWSCredentials();
            }
            catch
            {

            }
            options.DefaultClientConfig.BufferSize = 52428800;
            options.DefaultClientConfig.ServiceURL = "https://axrrofos01zp.compat.objectstorage.me-jeddah-1.oraclecloud.com";

            services.AddDefaultAWSOptions(options);
            services.AddAWSService<IAmazonS3>();
            services.AddHttpClient();
            services.AddMediatR(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "awsss v1"));
            }
            if(context.Database.GetPendingMigrations().Count() > 0)
            {
                await context.Database.MigrateAsync();
            }
            app.UseRouting();

            app.UseAuthorization();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Ensaf Scheduler Dashboard",
                Authorization = new[] { new HangfireAuthorizationFilter() }

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
