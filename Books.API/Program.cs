using Microsoft.EntityFrameworkCore;
using Books.DAL;
using Books.DAL.Repositories;
using Books.BLL.Services;


var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();

// Add services
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();

// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("AivenDb");
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();

// CORS - äîçâîëÿºìî ðåàêòó êèäàòè çàïèòè íà íàø áåê
string corsName = "allowAll";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsName, cfg =>
    {
        cfg.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS - äîçâîëÿºìî ðåàêòó êèäàòè çàïèòè íà íàø áåê
app.UseCors(corsName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.SeedAsync().Wait();

app.Run();
