using Core.Abstractions.Helpers;
using Core.Abstractions.Repository;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repository;
using Serilog;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Generic
builder.Services.AddCors();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //define which claim requires to check
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //store the value in appsettings.json
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// Repositories
builder.Services.AddTransient<IBetRepository, BetRepository>();
builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddTransient<IUserInfoRepository, UserInfoRepository>();

// Business
builder.Services.AddTransient<IBetService, BetService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>();
builder.Services.AddTransient<IUserInfoService, UserInfoService>();

// EF
string connectionString = builder.Configuration.GetConnectionString("SQLServer");
builder.Services.AddDbContext<IGamblingContext, GamblingContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);

// Configure Logger
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Debug);


builder.Host.UseSerilog((ctx, lc) =>
{
    lc.MinimumLevel.Warning()
    .WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<IGamblingContext>();
    var migrations = context.Database.GetPendingMigrations();
    if (migrations.Any())
    {
        context.Database.Migrate();
    }
}

app.UseCors(options =>
{
    options.WithOrigins("*");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
