using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Otter.Core.Repositories;
using Otter.Core.Services;
using Otter.API.Extensions;
using Otter.Core.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // Disable automatic model state validation
    options.SuppressModelStateInvalidFilter = true;
});

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];


// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options => {
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = jwtSettings["Issuer"],
//         ValidAudience = jwtSettings["Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
//         ClockSkew = TimeSpan.Zero
//     };
// });

builder.Services.AddAuthorization();


builder.Services.AddDbContext<PGSQLContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDependency();



builder.Services.AddCors(options =>
{
    // options.AddPolicy("AllowAllOrigins",
    //     builder => builder.AllowAnyOrigin()
    //                       .AllowAnyMethod()
    //                       .AllowAnyHeader()
    //                       .AllowCredentials());

    options.AddPolicy("AllowSelteApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Adjust the origin as needed
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSelteApp");
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();