using AIMY.Db.Prototype.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AIMY.Db.Prototype.Api.Configuration;
using AIMY.Db.Prototype.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var secretName = builder.Configuration.GetValue<string>("SecretsManager:SecretName") ?? "";
// Add services to the container.
builder.Configuration.AddSecretsManager(configurator: options =>
{
    options.SecretFilter = (secret) =>
        secret.Name.Contains(value: secretName);
    // Add the section names without the secret name as the prefix
    options.KeyGenerator = (l, s) => s.Substring(s.IndexOf(":", StringComparison.Ordinal) + 1);
});

// Configure AWS KMS options
builder.Services.Configure<AwsKmsOptions>(
    builder.Configuration.GetSection(AwsKmsOptions.SectionName));

// Register AWS KMS service
builder.Services.AddScoped<IAwsKmsService, AwsKmsService>();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
    //options.UseNpgsql(builder.Configuration.GetConnectionString("AWS")).UseSnakeCaseNamingConvention();
});

builder.Services.AddDbContextFactory<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
    //options.UseNpgsql(builder.Configuration.GetConnectionString("AWS")).UseSnakeCaseNamingConvention();
},ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
