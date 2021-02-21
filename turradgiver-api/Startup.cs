using DAL;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using turradgiver_api.Services;
using System.Text;
using System.Linq;
using System.Collections;
using Microsoft.OpenApi.Models;

namespace turradgiver_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Turradgiver API", Version = "v1",Description="APi about turradgiver" });
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",  
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header, 
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme 
                    { 
                    Reference = new OpenApiReference 
                    { 
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer" 
                    } 
                    },
                    new string[] { } 
                    } 
                });

            });
            services.AddDbContext<TurradgiverContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Turradgiver")));

            services.AddScoped(typeof(IRepository< >), typeof(Repository< >));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAddsService, AddsService>();


            services.AddAuthentication(options=> {
                // options.AuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
                }).AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:JWTKey").Value)),
                        // Could be good to add the ValidateIssuer and Validate Audience once we know the url
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }

            // app.UseHttpsRedirection();
            app.UseRouting();

            // Weird behavior happend if UseAuthorization is placed after UseAuthentification
            // Thing is that every end point with bearer auth will get a 401 instead of a valid authentfication
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
