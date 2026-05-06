using System.Reflection;
using LibrarySystem.Data;
using LibrarySystem.Extensions;
using LibrarySystem.Middleware;
using LibrarySystem.Services.Implementations;
using LibrarySystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithXml(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<ILoanService, LoanService>();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "System biblioteczny API";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "System biblioteczny API v1");
});
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

app.Run();
