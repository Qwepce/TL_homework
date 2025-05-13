using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Infrastructure;
using WebAPI.Web.Mappings;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions( options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            Dictionary<string, string[]> errors = context.ModelState
                .Where( e => e.Value.Errors.Count > 0 )
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select( e => e.ErrorMessage ).ToArray()
                );

            return new BadRequestObjectResult( new { Errors = errors } );
        };
    } );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayerBindings();
builder.Services.AddInfrastructureLayerBindings( builder.Configuration );
builder.Services.AddWebMappingBindings();

builder.Services.AddRouting( options =>
{
    options.LowercaseUrls = true;
} );

var app = builder.Build();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();