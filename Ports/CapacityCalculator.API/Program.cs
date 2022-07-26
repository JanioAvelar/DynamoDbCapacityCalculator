using CapacityCalculator.Application.Services;
using CapacityCalculator.Domain.Interface;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(
    o => 
    {
        o.SerializerSettings.Converters.Add(
        new StringEnumConverter
        { 
            CamelCaseText = true 
        });    
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

//Dependencies
builder.Services.AddScoped<IReadCapacityCalculator, RCUCalculator>();
builder.Services.AddScoped<IWriteCapacityCalculator, WCUCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
