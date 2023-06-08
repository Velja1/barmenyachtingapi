using BarmenYachting.Api.Emails;
using BarmenYachting.Application.Emails;
using BarmenYachting.Application.Logging;
using BarmenYachting.Application.UseCases.Commands;
using BarmenYachting.Application.UseCases.Queries;
using BarmenYachting.DataAccess;
using BarmenYachting.Implementation;
using BarmenYachting.Implementation.Emails;
using BarmenYachting.Implementation.Logging;
using BarmenYachting.Implementation.UseCases.Commands;
using BarmenYachting.Implementation.UseCases.Queries;
using BarmenYachting.Implementation.Validators;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BarmenYachting.API
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
            services.AddTransient<IGetManufactersQuery, EfGetManufactersQuery>();
            services.AddTransient<IGetVesselsQuery, EfGetVesselsQuery>();
            services.AddTransient<IGetVesselQuery, EfGetVesselQuery>();
            services.AddTransient<ICreateManufacterCommand, EfCreateManufacterCommand>();
            services.AddTransient<ICreateVesselCommand, EfCreateVesselCommand>();
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<IDeleteManufacterCommand, EfDeleteManufacterCommand>();
            services.AddTransient<IDeleteVesselCommand, EfDeleteVesselCommand>();
            services.AddTransient<IDbLogger, DbActionLogger>();
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<CreateManufacterValidator>();
            services.AddTransient<CreateVesselValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<IEmailSender, TestEmailSender>();
            services.AddTransient<IEmailSender>(x => new SmtpEmailSender("veljkovulovictest@gmail.com", "xxvpkkexlswsrwgj", 587, "smtp.gmail.com"));
            services.AddDbContext<BarmenYachtingDbContext>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BarmenYachting.API",
                                                     Description = "BarmenYachting API project for college",
                                                     Version = "v1", Contact = new OpenApiContact
                                                                    {
                                                                        Name = "Veljko Vulovic",
                                                                        Email = "veljko.vulovic.282.18@ict.edu.rs"
                                                                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarmenYachting.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
