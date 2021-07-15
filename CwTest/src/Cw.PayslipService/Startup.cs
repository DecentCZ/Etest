using Cw.PayslipData;
using Cw.PayslipLogic;
using Cw.PayslipLogic.Interfaces;
using Cw.PayslipCommon;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using Cw.Platform.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Cw.PayslipService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "V1",
                    Title = "Payslip"
                });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Auth header is using bearer",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                      {
                        {
                          new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              }
                            },
                            new List<string>()
                          }
                        });
            });


            services.AddSwaggerGenNewtonsoftSupport();
            services.AddOptions();
            services.Configure<AdminConfig>(Configuration.GetSection(typeof(AdminConfig).Name));

            services.AddScoped<IEmployeeLogic, EmployeeLogic>();
            //services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPayslipLogic, Cw.PayslipLogic.PayslipLogic>();
            services.AddDbContext<PayslipDBContext>(options =>
            options.UseInMemoryDatabase(databaseName: "Payslip"));
            services.AddScoped<PayslipDBContext>();

            services.Configure<TaxPayslipConfig>(Configuration.GetSection(nameof(TaxPayslipConfig)));

            services.AddSingleton<IPayslipConfigLogic, PayslipConfigLogic>(o =>
            {

                var payslipConfigLogic = new PayslipConfigLogic();
                var taxPayslipConfigList = new TaxPayslipConfig();
                var taxPayslipConfigSection = Configuration.GetSection(nameof(TaxPayslipConfig));
                taxPayslipConfigSection.Bind(taxPayslipConfigList);
                var taxPayslipConfig = new TaxPayslipConfig();
                foreach (var config in taxPayslipConfigList.TaxRates)
                {
                    payslipConfigLogic.TaxRates.Add(new TaxRates { Salary = config.Salary, Tax = config.Tax });
                }
                return payslipConfigLogic;
            });
            var secretKey = PayslipConstants.Key;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenParam = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireSignedTokens = false,
                RequireExpirationTime = false,
                ValidateLifetime = false
            };
            services.AddAuthentication(
                a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenParam;
                    options.SaveToken = true;
                }
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "Payslip"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
