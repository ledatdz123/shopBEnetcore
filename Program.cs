using shopEcomerceExBE.Common.BaseService;
using shopEcomerceExBE.Repos.UserRepos;
using shopEcomerceExBE.Service.JwtService;
using shopEcomerceExBE.Service.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IBaseService, BaseServiceImpl>();
builder.Services.AddScoped<IUserRepos, UserReposImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IJwtService, JwtServiceImpl>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
