using Anzen.Data;
using Anzen.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

//Get all submissions with pagination and sorting
app.MapGet("/submissions", async ( HttpContext httpContext, int page = 1, int pageSize = 10, string columnToSort = "Id", bool asc = true) =>
{
    //Create a dictionary (map) with all possible columns to sort
    //I didn't include Premium ($) because SQLite doesn't support ordering by decimals
    var columnsToSortDictionary = new Dictionary<string, Expression<Func<Submission, object>>>
    {
        ["Id"] = s => s.Id,
        ["AccountName"] = s => s.AccountName,
        ["UwName"] = s => s.UwName,
        ["EffectiveDate"] = s => s.EffectiveDate,
        ["ExpirationDate"] = s => s.ExpirationDate,
        ["Sic"] = s => s.Sic
    };

    //Check if the columnToSort exists in the dictionary
    if (!columnsToSortDictionary.ContainsKey(columnToSort))
    {
        return Results.BadRequest($"Invalid columnToSort name: {columnToSort}");
    }

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SatoriContext>();
    var submissions = context.Submission //Return Submission information
    .Include(s => s.Status) //Include the Status  of the Submission
    .Include(s => s.Coverages) //Include the Coverages of the Submission

    //Paginate the results
    .Skip((page - 1) * pageSize) 
    .Take(pageSize);

    //Sort the results by one of the columns in the dictionary
    if (asc)
        submissions = submissions.OrderBy(columnsToSortDictionary[columnToSort]);
    else
        submissions = submissions.OrderByDescending(columnsToSortDictionary[columnToSort]);

    return Results.Ok(await submissions.ToListAsync());
});

//Get a specific submission
app.MapGet("/submissions/{id}", async (string id, HttpContext httpContext) =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SatoriContext>();
    var submission = await context.Submission //Return Submission information
        .Include(s => s.Status) //Include the Status  of the Submission
        .Include(s => s.Coverages) //Include the Coverages of the Submission
        .FirstOrDefaultAsync(s => s.Id == int.Parse(id));

    if (submission is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(submission);
});

//Search all with pagination
app.MapGet("/search/{search}", async (string search, HttpContext httpContext, int page = 1, int pageSize = 10) =>
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<SatoriContext>();
    var submissions = await context.Submission //Return Submission information
        .Include(s => s.Status) //Include the Status  of the Submission
        .Include(s => s.Coverages)  //Include the Coverages of the Submission

        //Search by AccountName, UwName, Premium($), EffectiveDate, ExpirationDate, Sic
        .Where(s => s.AccountName.ToLower().Contains(search.ToLower())
        || s.UwName.ToLower().Contains(search.ToLower())
        || s.Premium.ToString().Contains(search)
        || s.EffectiveDate.ToString().ToLower().Contains(search.ToLower())
        || s.ExpirationDate.ToString().ToLower().Contains(search.ToLower())
        || s.Sic.ToLower().Contains(search.ToLower()))

        //Paginate the results
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    if (submissions is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(submissions);
});


app.Run();