using MongoDBFactory.API.Constants;
using MongoDBFactory.API.DependencyInjection;
using MongoDBFactory.API.Filters;
using MongoDBFactory.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers(options => options.Filters.AddService<NotificationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<UnexpectedErrorMiddleware>();
}

app.UseCors(CorsPoliciesNamesConstants.CorsPolicy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
