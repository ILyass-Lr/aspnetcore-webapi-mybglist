using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");

if(app.Configuration.GetValue<bool>("UseScalar"))
    app.MapScalarApiReference();


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/error", () => Results.Problem());
app.MapGet("/error/test", () => { throw new Exception("test"); });

app.MapControllers();

app.Run();
