using Anzen.Data;
using Anzen.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SatoriContext>(options => 
    options.UseSqlite("Data Source=satori.db"));

var app = builder.Build();

// Configuration of the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Note: On bigger projects, I would use a controller pattern

app.MapGet("/submissions", async (
HttpContext httpContext, 
int page = 1, 
int pageSize = 10, 
string column = "Id", 
bool asc = true) =>
{
    //Create a map with a string key that stores a lampda expression
    //I didn't include Premium$ because SQLite doesn't support ordering by decimals
    var columnMap = new Dictionary<string, Expression<Func<Submission, object>>>
    {
        ["Id"] = s => s.Id,
        ["AccountName"] = s => s.AccountName,
        ["UwName"] = s => s.UwName,
        ["EffectiveDate"] = s => s.EffectiveDate,
        ["ExpirationDate"] = s => s.ExpirationDate,
        ["Sic"] = s => s.Sic
    };

    //check if the column exists in the map
    if (!columnMap.ContainsKey(column))
    {
        return Results.BadRequest($"Invalid column name: {column}");
    }

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SatoriContext>();
    var submissions = context.Submission
    .Include(s => s.Status)
    .Include(s => s.Coverages)
    .Skip((page - 1) * pageSize)
    .Take(pageSize);

    if (asc)
        submissions = submissions.OrderBy(columnMap[column]);
    else
        submissions = submissions.OrderByDescending(columnMap[column]);

    var result = await submissions.ToListAsync();

    return Results.Ok(result);
});


app.MapGet("/submissions/{id}", async (string id, HttpContext httpContext) =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SatoriContext>();
    var submission = await context.Submission
        .Include(s => s.Status)
        .Include(s => s.Coverages)
        .FirstOrDefaultAsync(s => s.Id == int.Parse(id));

    if (submission is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(submission);
});

app.Run();