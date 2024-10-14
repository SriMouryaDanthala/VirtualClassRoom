using Microsoft.EntityFrameworkCore;
using Persistence.DBContext;
using System.Reflection;
using VirtualClassRoomMediator.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddScoped<VirtualClassRoomDbContext>();*/
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VirtualClassRoomDbContext>(
        options => options.UseNpgsql(ConnectionString));
builder.Services.AddScoped<UserRoleDbContext>();
builder.Services.RegisterMediators(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();  
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
