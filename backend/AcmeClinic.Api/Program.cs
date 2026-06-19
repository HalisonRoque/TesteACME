using AcmeClinic.Infrastructure.Context;
using AcmeClinic.Domain.Interfaces;
using AcmeClinic.Infrastructure.Repositories;
using AcmeClinic.Application.Services;
using AcmeClinic.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//Conexão na Base da Dados 
builder.Services.AddSingleton(
new DbSQLiteContext(
builder.Configuration
.GetConnectionString(
"DefaultConnection"
)!
));

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();

builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<AtendimentoService>();

builder.Services.AddCors(
options =>
{
    options.AddPolicy(
        "frontend",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(
                    "http://localhost:4200"
                );
        }
    );
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseCors("frontend");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

// Controllers
app.MapControllers();

using var scope =
app.Services.CreateScope();

DatabaseInitializer.Initialize(
scope.ServiceProvider
.GetRequiredService
<DbSQLiteContext>()
);

app.Run();