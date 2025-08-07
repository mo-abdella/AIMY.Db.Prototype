//using AIMY.Db.Prototype.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Configuration.AddSecretsManager(configurator: options =>
{
    options.SecretFilter = (secret) =>
        secret.Name.Contains(value: builder.Configuration.GetValue<string>("SecretsManager:SecretName") ??
                                    throw new InvalidOperationException());
    // Add the section names without the secret name as the prefix
    options.KeyGenerator = (l, s) => s.Substring(s.IndexOf(":", StringComparison.Ordinal) + 1);
});

Console.WriteLine("Using connection string: " + builder.Configuration.GetConnectionString("DefaultConnection"));

//builder.Services.AddDbContext<MyDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
//    //options.UseNpgsql(builder.Configuration.GetConnectionString("AWS")).UseSnakeCaseNamingConvention();
//    options.LogTo(Console.WriteLine);
//    options.EnableSensitiveDataLogging();
//});

//builder.Services.AddDbContextFactory<MyDbContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
//    //options.UseNpgsql(builder.Configuration.GetConnectionString("AWS")).UseSnakeCaseNamingConvention();
//    options.LogTo(Console.WriteLine);
//    options.EnableSensitiveDataLogging();
//}, ServiceLifetime.Scoped);


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
