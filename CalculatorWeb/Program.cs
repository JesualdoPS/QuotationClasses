using Calc.BusinessLogic;
using Calc.Persistance;
using Contracts;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:7299", "https://localhost:5044");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<ICalculator, Calculator>();
builder.Services.AddTransient<IRepository, RepositorySQL>();
builder.Services.AddOpenApiDocument(options => {
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "V2",
            Title = "IQuantity Calculator",
            Description = "A calculator for both natural numbers and IQuantity",
            TermsOfService = "http://localhost7299/terms",
            Contact = new OpenApiContact
            {
                Name = "Development Group",
                Url = "http://localhost7299/website"
            },
            License = new OpenApiLicense
            {
                Name = "Example Licence",
                Url = "http://localhost7299/license"
            }
        };
    };
});
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
