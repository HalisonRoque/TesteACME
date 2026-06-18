using AcmeClinic.Infrastructure.Context;

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

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

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