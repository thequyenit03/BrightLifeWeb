using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Service.Heplers;
using Service.Models;
using Service.Services;
using Service.Services.Interface;
using System.Reflection.Emit;

namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<Models.ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                    };
                });
            // Register services
            builder.Services.AddScoped<IJwtService,JwtService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            var app = builder.Build();
            SeedingData(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }

        private static void SeedingData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Models.ApplicationDbContext>();
                if (dbContext.Database.EnsureCreated())
                {
                    // Database was created, seed data
                    User[] users = new User[]
{ 
                        new User
                        {
                            FullName = "Admin User",
                            Email = "admin@gmail.com",
                            Address = "123 Admin St",
                            PhoneNumber = "1234567890",
                            Gender = true,

                        }
        };
                            users[0].PasswordHash = HelperHashingPassword.HashPassword("Admin@1234");
                    dbContext.Users.AddRange(users);
                    Role[] roles = new Role[]
                            {
                                new Role
                                {
                                    Name = "Admin",
                                    Description = "Administrator role with full access"
                                },
                            };
                    dbContext.Roles.AddRange(roles);
                    UserRole[] userRoles = new UserRole[]
                            {
                        new UserRole
                        {
                            UserId = users[0].Id,
                            RoleId = 1 // Assuming role with Id 1 exists
                        }
                            };
                    dbContext.UserRoles.AddRange(userRoles);
                    dbContext.SaveChanges(); // Save changes to the database

                    dbContext.Database.Migrate(); // Apply migrations if any
                    Console.WriteLine("--> Database created and migrations applied.");
                }
                else
                {
                    Console.WriteLine("--> Database already exists, no seeding required.");
                }
            }
        }
    }
}
