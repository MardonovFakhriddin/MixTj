using Infrastructure.Data;
using Infrastructure.Extensions;
using Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);
builder.Services.SwaggerConfigurationServices();
builder.Services.AuthConfigureServices(builder.Configuration);



var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var datacontext = serviceProvider.GetRequiredService<DataContext>();
    await datacontext.Database.MigrateAsync();

    var seeder = serviceProvider.GetRequiredService<Seeder>();
    await seeder.SeedRole();
    await seeder.SeedUser();
}
catch (Exception e)
{
    // 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();