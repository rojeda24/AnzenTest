using Anzen.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SatoriContext>(options => 
    options.UseSqlite("Data Source=satori.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/submissions", async (SatoriContext context, int page = 1, int pageSize = 10) =>
{
    var submissions = await context.Submission
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();

    return submissions;
});


app.MapGet("/submissions/{id}", async (string id, HttpContext httpContext) =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SatoriContext>();
    var submission = await context.Submission
            .FirstOrDefaultAsync(s => s.Id == int.Parse(id));

    if (submission is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(submission);
});

app.Run();