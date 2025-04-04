using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/registration", (User user) =>
{
    return "Register";
}).WithParameterValidation();

app.MapPost("/registration", ([FromBody] User user) =>
{
    return "Register";
}).WithParameterValidation();

app.Run();
