using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using OnlineStore.Services;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registrar o IFirebaseClient para injeção de dependência
builder.Services.AddSingleton<IFirebaseClient>(sp =>
{
    IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = builder.Configuration["Firebase:AuthSecret"],  // Pegue do appsettings.json ou variáveis de ambiente
        BasePath = builder.Configuration["Firebase:BasePath"]      // Pegue do appsettings.json ou variáveis de ambiente
    };
    return new FirebaseClient(config);
});

// Registrar o ItemService no contêiner de injeção de dependência
builder.Services.AddScoped<ItemService>();

// Adicionar controladores
builder.Services.AddControllers();

// Configurar o Swagger (para documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();