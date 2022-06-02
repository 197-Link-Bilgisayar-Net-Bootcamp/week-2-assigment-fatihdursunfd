using API.Data;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using API.Validations;
using FluentValidation.AspNetCore;
using API.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().
    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<Context>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("BootcampUserDbConnection")));

builder.Services.AddScoped<IUserRepository , UserRepository>();
builder.Services.AddScoped<IUserService , UserService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
