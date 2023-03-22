using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
var app = builder.Build();

app.UseHttpsRedirection();

app.UseSpa(spaBuilder =>
{
    spaBuilder.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment()) spaBuilder.UseAngularCliServer("start");
});

app.Run();