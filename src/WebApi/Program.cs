using HtmlAgilityPack;
using WebApiAbstraction.Repositories;
using WebApiAbstraction.Services;
using WebApiInfrastructure.Repositories;
using WebApiService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(HtmlWeb));
builder.Services.AddScoped<ILatestNewRepository, LatestNewRepository>();
builder.Services.AddTransient<ILatestNewService, LatestNewService>();

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