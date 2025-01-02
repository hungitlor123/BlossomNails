using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data.UnitOfWork.Implementations;
using Data.UnitOfWork.Interfaces;
using Domain.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlossomNailsContext>(options => options.UseSqlServer(sqlConnectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

