using Microsoft.EntityFrameworkCore;
using Persistence.DBContext;
using VirtualClassRoom.Mediator;
using VirtualClassRoomMediator.Mediators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddScoped<VirtualClassRoomDbContext>();*/
builder.Services.AddDbContext<VirtualClassRoomDbContext>(
        options => options.UseNpgsql("name=ConnectionStrings:PostgresqlConnection"));
builder.Services.AddScoped<UserRoleDbContext>();
builder.Services.AddScoped<VirtualClassRoomMediator.Mediators.UserRoleMediator>();

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
