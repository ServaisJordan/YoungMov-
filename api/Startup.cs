using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL;
using Swashbuckle.AspNetCore.Swagger;
using api.Infrastructure;

namespace api
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
            services.AddDbContext<smartCityContext>(config => config.UseSqlServer(Configuration.GetConnectionString("SmartCity")));
            services.AddTransient<DataAccess>();
            AutoMapper.Mapper.Initialize(config => config.AddProfile<Infrastructure.MappingProfile>());
            services.AddAutoMapper();

            string SecretKey = Configuration.GetValue<string>("SecretKey");
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
            services.Configure<JwtIssuerOptions>(options =>
        {
            options.Issuer = "MonSuperServeurDeJetons";
            options.Audience = "http://localhost:5000";
            options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        });



            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "MonSuperServeurDeJetons",

                ValidateAudience = true,
                ValidAudience = "http://localhost:5000",

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(
                options =>
                {
                        
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.Audience = "http://localhost:5000";
                        options.ClaimsIssuer = "MonSuperServeurDeJetons";
                        options.TokenValidationParameters = tokenValidationParameters;
                        options.SaveToken = true;
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            
            services.AddMvc(options =>
            { 
                options.Filters.Add(typeof(api.Infrastructure.BusinessExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
