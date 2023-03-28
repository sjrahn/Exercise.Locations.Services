using Exercise.Locations.Services.Controllers;
using Exercise.Locations.Services.Services;

var app = ApplicationBuilderService.ConfigureApp(args).Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.MapLocationEndpoints();

app.Run();