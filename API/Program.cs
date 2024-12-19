using API.Service;
using Microsoft.Data.SqlClient; 
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();

builder.Services.AddScoped<DbConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

builder.Services.AddScoped<ITableAService, TableAService>();
builder.Services.AddScoped<ITableBService, TableBService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();