using System.Reflection;
using FluentValidation;
using MediatR;
using Shop.Api.HandlerMiddware;
using Shop.Aplication.Mapper;
using Shop.Aplication.Validation;
using Shop.Domain.RequestModel;
using Shop.Infratructure.Dependency;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HandErrorMiddleware>();
builder.Services.AddInfratrcuture(builder.Configuration);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(HandCreateUserCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddAutoMapper(typeof(MapData).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidation).Assembly) ;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<HandErrorMiddleware>();
app.MapControllers();

app.Run();
