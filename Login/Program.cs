using FluentValidation;
using Login.AutoMapper;
using Login.DB;
using Login.Models;
using Login.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ContextModel>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS")));
builder.Services.AddScoped<IValidator<UserDto>, UserValidation>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddAutoMapper(typeof(AutomaMapperConfig));
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
var AuthenticationStettings = new AuthenticationSttetings(); // jwt stettings
builder.Configuration.GetSection("Authentication").Bind(AuthenticationStettings);
builder.Services.AddSingleton(AuthenticationStettings);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = "Bearer";
    opt.DefaultScheme = "Bearer";
    opt.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = AuthenticationStettings.JwtIssuer,
        ValidAudience = AuthenticationStettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationStettings.JwtKey)),
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
