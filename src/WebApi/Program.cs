using HtmlAgilityPack;
using WebApiAbstraction.Repositories;
using WebApiApplication;
using WebApiApplication.Services;
using WebApiInfrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(HtmlWeb));
builder.Services.AddScoped<ILatestNewRepository, LatestNewRepository>();
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddCors(opt=>
{
    opt.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();