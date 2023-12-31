using Anzen.Data;
using Microsoft.EntityFrameworkCore;

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
app.MapGet("/submissions", async (SatoriContext context, int page = 1, int pageSize = 10) =>
{
    var submissions = context.Submission
    .Include(s => s.Status)
    .Include(s => s.Coverages)
    .Skip((page - 1) * pageSize)
    .Take(pageSize);

    if (true)
        submissions.OrderBy(s => s.AccountName);
    else
        submissions = submissions.OrderByDescending(s => s.AccountName);

    var result = await submissions.ToListAsync();

    return result;
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