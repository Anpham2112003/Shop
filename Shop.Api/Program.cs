using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shop.Api.Controllers;
using Shop.Aplication.Commands;
using Shop.Aplication.Mapper;
using Shop.Aplication.Validation;
using Shop.Domain.Options;
using Shop.Infratructure.Dependency;
using Serilog;
using Microsoft.OpenApi.Models;
using Shop.Aplication.Commands.AuthCommand;
using Shop.Aplication.Behavior;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddSerilog();

builder.Services.AddControllers()
    .AddJsonOptions(op => op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ValidationMiddware>();
builder.Services.AddInfratrcuture(builder.Configuration);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//MediatR config
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    
});


//Auto Mapper config
builder.Services.AddAutoMapper(op =>
{
    op.AddMaps(typeof(MapData).Assembly);
   
});


//Validation config
builder.Services.AddValidatorsFromAssembly(typeof(CreateAddressValidation).Assembly) ;

builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(op =>    //JWT config
{
    op.SaveToken = true;
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime= true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:IssuerSigningKey"])),
        ValidAudience = JwtOptions.Audienc,
        ValidIssuer = JwtOptions.IssUser   
    };
    
});


builder.Services.AddOptions<VnPayOptions>()
    .Bind(builder.Configuration.GetSection(VnPayOptions.Method));
builder.Services.AddSignalR();
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidationMiddware>();
app.MapControllers();



app.Run();
