using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using OnlineStore.Mapper; // Importar todos os profiles
using OnlineStore.Repositories;
using OnlineStore.Services;
using Microsoft.OpenApi.Models;
using OnlineStore.Mapper.ItemProfile;

var builder = WebApplication.CreateBuilder(args);

// Definir a variável de ambiente para as credenciais do Google Firestore
string path = "C:\\Users\\Lucas\\Desktop\\OnlineStore\\OnlineStore\\Secrets\\onlinestore-fde01-fb4da4a993da.json";
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

// Configurar o Swagger para aceitar JWT Bearer Token
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Inicializar Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(path)
});

// Configurar autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/onlinestore-fde01"; // Substitua pelo seu Firebase project ID
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/onlinestore-fde01", // Substitua pelo seu Firebase project ID
            ValidateAudience = true,
            ValidAudience = "onlinestore-fde01", // Substitua pelo seu Firebase project ID
            ValidateLifetime = true
        };
    });

// Adicionar autenticação e autorização
builder.Services.AddAuthorization();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(UserProfile), typeof(ItemProfile)); // Adicionar todos os perfis de mapeamento

// Registrar FirestoreDb como singleton (banco de dados do Firebase)
builder.Services.AddSingleton(sp =>
{
    return FirestoreDb.Create("onlinestore-fde01");
});

// Registrar os repositórios e serviços
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<AuthService>();  // Serviço de autenticação
builder.Services.AddScoped<IUserRepository, UserRepository>();  // Repositório de usuários
builder.Services.AddScoped<UserService>();  // Serviço para usuários

// Adicionar controladores
builder.Services.AddControllers();

// Configurar Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar HTTPS, autenticação e autorização
app.UseHttpsRedirection();
app.UseAuthentication(); // Habilitar autenticação JWT
app.UseAuthorization();  // Habilitar autorização

app.MapControllers();

app.Run();