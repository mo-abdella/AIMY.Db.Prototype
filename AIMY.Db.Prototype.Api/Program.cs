using AIMY.Db.Prototype.Infrastructure.Context;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

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

builder.Services.AddDbContext<MyDbContext>(options =>
{
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
    //options.UseNpgsql(builder.Configuration.GetConnectionString("AWS")).UseSnakeCaseNamingConvention();
});

builder.Services.AddDbContextFactory<MyDbContext>(options =>
{
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
    //options.UseNpgsql(builder.Configuration.GetConnectionString("AWS")).UseSnakeCaseNamingConvention();
},ServiceLifetime.Scoped);

AmazonKeyManagementServiceClient kmsClient = new();

EncryptRequest encryptRequest = new()
{
    KeyId = "0b1e25ac-e535-4088-a507-79ac4beb88f3",
    Plaintext = new MemoryStream(Encoding.UTF8.GetBytes(s: "q3n8cCANrXlDCIg8fpYX6WSsBmagkqiOV3mGCBh3"))
};

EncryptResponse response = await kmsClient.EncryptAsync(request: encryptRequest);

byte[] encryptedSecret;

using (MemoryStream ms = new())
{
    response.CiphertextBlob.CopyTo(destination: ms);
    encryptedSecret = ms.ToArray();
}

var seceret = encryptedSecret;

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
