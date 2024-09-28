using dotnet_todo_api.src.TodoApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace dotnet_todo_api
{
    public class Startup(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure PostgreSQL with Entity Framework Core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddSwaggerGen();

            string? secretKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("Jwt Key is missing!");
            }

            // Add JWT Authentication
            _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Jwt:Issuer"], // Read from environment variable
                        ValidAudience = _configuration["Jwt:Audience"], // Read from environment variable                        
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // Read secret key from environment variable
                    };
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Enable authentication middleware
            app.UseAuthentication();

            // Enable authorization middleware
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}