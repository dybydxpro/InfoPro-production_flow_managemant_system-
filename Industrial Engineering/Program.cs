using System.Text;
using Industrial_Engineering.Data;
using Industrial_Engineering.Services;
using Industrial_Engineering.Services.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddDbContext<IndustrialEngineeringDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnecion")));

builder.Services.AddTransient<IIndustrialEngineeringUnitOfWork, IndustrialEngineeringUnitOfWork>();
builder.Services.AddTransient<IProductionFloorService, ProductionFloorService>();
builder.Services.AddTransient<IStyleService, StyleService>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
            ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
        };
    }
    );

// Add Principle
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InfoPro IndustrialEngineering", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InfoPro IndustrialEngineering v1"));

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();
