
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using ERP.Data;

var builder = WebApplication.CreateBuilder(args);
    
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new DatabaseHelper(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.MapGet("/parts", async (IDatabaseHelper databaseHelper) =>
{
    try
    {
        var parts = databaseHelper.GetParts();
        return Results.Ok(parts);
    }
    catch (Exception ex)
    {
        // Log the exception details as necessary
        return Results.Problem("An error occurred while fetching parts data.");
    }
});

app.MapGet("/boms", async (IDatabaseHelper databaseHelper) =>
{
    try
    {
        var boms = databaseHelper.GetBOMs();
        return Results.Ok(boms);
    }
    catch (Exception ex)
    {
        // Log the exception details as necessary
        return Results.Problem("An error occurred while fetching BOMs data.");
    }
});

app.Run();
