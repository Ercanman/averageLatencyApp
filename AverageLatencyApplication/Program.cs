using AverageLatencyApplication;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Average Latency API",
        Version = "v1",
        Description = "API for calculating average latency metrics across external services.",
        Contact = new OpenApiContact
        {
            Name = "Average Latency Team",
            Email = "support@averagelatency.local"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }

    options.SupportNonNullableReferenceTypes();
});

builder.Services.RegisterServices();
builder.Services.ConfigureHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Average Latency API v1");
        options.DisplayRequestDuration();
        options.DefaultModelsExpandDepth(0);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
