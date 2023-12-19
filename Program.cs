using auction.Data;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionConnectionString"));
});

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
  options.AllowAnyHeader();
  options.AllowAnyOrigin();
  options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
