using Books.API.Settings;
using Books.BLL.Services;
using Books.DAL;
using Books.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<GenreRepository>();

// Add services
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<GenreService>();

//Add automapper
string automapperKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA0NzIzMjAwIiwiaWF0IjoiMTc3MzI0MTA3OCIsImFjY291bnRfaWQiOiIwMTljZGQ2NWYxZWQ3OTA3YjcyYjRhMDNiNjViMTkzZiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa2tlcGY0ZTdqcDRjenZkdjJyaHN6eXg0Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.svKaWOFtPPPjBYzBIH7ggsnLcM_yAph6LDEp9I2BXoewAfgLI9-IKPVjmkLxEGbDWjQun7pbhwnoAtEoc2hMKDxYf6mmFL2I4QwfS7TFHHjEIQgjriB1AiIylPEfTxoGCskPfh2dcacJlOMiDcR7OV8eL6_fR2puUaCZxGbOa4nsLGhqmygMdQULWLxlhUWbiik8zWS278P6BincBDGyhL_PnODzfl11CXVIU_0iSNamBDMmPfgb_K3PkU45THey8swhDVA93Zfiwiu7uwiVv2Gze0Mc1i3UZ0XDE9bOfN18uBVl7dwvK9wRENlYCqBRulgI0r97vHrj17EvmdqTaA";
builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = automapperKey;

}, AppDomain.CurrentDomain.GetAssemblies());

// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? localDb = builder.Configuration.GetConnectionString("LocalDb");
    string? aivenDb = builder.Configuration.GetConnectionString("AivenDb");
    options.UseNpgsql(aivenDb);
});

builder.Services.AddControllers();

// CORS - ���������� ������ ������ ������ �� ��� ���
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

// CORS - ���������� ������ ������ ������ �� ��� ���
app.UseCors(corsName);

app.UseHttpsRedirection();

// Static files
string root = app.Environment.ContentRootPath;
string storagePath = Path.Combine(root, StaticFilesSettings.StorageDir);

// Books
string _booksPath = Path.Combine(storagePath, StaticFilesSettings.BooksDir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(_booksPath),
    RequestPath = StaticFilesSettings.BookUrl
});

//Authors
string authorsPath = Path.Combine(storagePath, StaticFilesSettings.AuthorsDir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(authorsPath),
    RequestPath = StaticFilesSettings.AuthorUrl
});

// Share
string sharePath = Path.Combine(storagePath, StaticFilesSettings.ShareDir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(sharePath),
    RequestPath = StaticFilesSettings.ShareUrl
});

app.UseAuthorization();

app.MapControllers();

//app.SeedAsync().Wait();

app.Run();