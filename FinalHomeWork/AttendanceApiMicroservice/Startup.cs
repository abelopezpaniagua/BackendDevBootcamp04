using AttendanceApiMicroservice.DbContext;
using AttendanceApiMicroservice.Profiles;
using AttendanceApiMicroservice.Services;
using Domain;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;

namespace AttendanceApiMicroservice
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
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(Configuration.GetConnectionString("rabbitmq")), h =>
                    {
                        h.Username(RabbitMqConsts.Username);
                        h.Password(RabbitMqConsts.Password);
                    });
                }));
            });
            services.AddMassTransitHostedService();

            services.Configure<AttendanceDatabaseSettings>(
                Configuration.GetSection(nameof(AttendanceDatabaseSettings)));

            services.AddSingleton<IAttendanceDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AttendanceDatabaseSettings>>().Value);

            services.AddSingleton<UserAttendanceService>();

            services.AddAutoMapper(typeof(UserAttendanceProfile));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AttendanceApiMicroservice", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AttendanceApiMicroservice v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
