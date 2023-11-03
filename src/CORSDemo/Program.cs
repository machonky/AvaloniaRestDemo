using CORSDemo.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// This is required to permit the AvaloniaBrowser UI to connect.
builder.Services.AddDevelopmentCorsOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDevelopmentCors(); // Must be called after UseRouting and before UseAuthorization

//app.UseAuthorization();

app.MapControllers();

app.Run();
