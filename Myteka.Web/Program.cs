using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Myteka.Infrastructure.Data;
using Myteka.Infrastructure.Data.Implementations;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.InternalModels.Users;
using Myteka.Search.Implementations;
using Myteka.Search.Interfaces;
using Myteka.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=Myteka.Test;Username=postgres;Password=str33tf1ght3r");
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=Myteka.Test;Username=postgres;Password=str33tf1ght3r");
});

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#region Repositories

builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();

#endregion

#region Search services

builder.Services.AddScoped<IAuthorSearch, AuthorSearch>();
builder.Services.AddScoped<IBookSearch, BookSearch>();
builder.Services.AddScoped<IContentSearch, ContentSearch>();

#endregion

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Добавляет поддержку работы с datetime в базе данных.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();