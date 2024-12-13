using Calc.BusinessLogic;
using Calc.Persistance;
using Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ICalculator, Calculator>();
builder.Services.AddTransient<IRepository, RepositorySQL>();
builder.Services.AddOpenApiDocument();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//object value = builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
