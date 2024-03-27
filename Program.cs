using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SRSWebApi.Data;
using SRSWebApi.Interfaces;
using SRSWebApi.Repository;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
	});

	c.OperationFilter<SecurityRequirementsOperationFilter>();
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "srs.webapi", Version = "1.0.0" });

});


builder.Services.AddAuthentication().AddJwtBearer(
	options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			ValidateLifetime = true,
			ValidateAudience = false,
			ValidateIssuer = false,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				builder.Configuration.GetSection("JWT:Secret").Value!)),
			ClockSkew = TimeSpan.Zero
		};
	}
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();


builder.Services.AddDbContext<SrsContext>(options =>
		options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
