var builder = WebApplication.CreateBuilder(args);

// Přidejte služby pro controllers a Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Použijte Swagger v režimu vývoje
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapování rout pro controllers
app.MapControllers();

app.Run();
