
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

builder.Services.AddScoped<DatabaseInitializer>(serviceProvider =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                           ?? builder.Configuration.GetConnectionString("DefaultConnection");
    return new DatabaseInitializer(connectionString);
});


builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>(provider =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                           ?? builder.Configuration.GetConnectionString("DefaultConnection");
    return new DatabaseHelper(connectionString);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});


var app = builder.Build();
// Run database initializer
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var databaseInitializer = services.GetRequiredService<DatabaseInitializer>();
    databaseInitializer.InitializeDatabase();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

    
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
