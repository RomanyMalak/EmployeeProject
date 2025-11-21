
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestProject.Context;

namespace TestProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<App_context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));

            builder.Services .AddIdentity<NewUser, IdentityRole>().AddEntityFrameworkStores<App_context>().AddDefaultTokenProviders();


            builder.Services.AddAuthentication(optiion => {
                optiion.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                optiion.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                optiion.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]));

                option.TokenValidationParameters = new TokenValidationParameters

                {
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    // ValidIssuer = "http://localhost:5290/",
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"]
                    // ValidAudience = "http://localhost:4200/"
                };


            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
