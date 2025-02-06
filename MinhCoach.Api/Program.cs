using MinhCoach.Api;
using MinhCoach.App;
using MinhCoach.Infra;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()   
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseHttpsRedirection(); 
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.MapControllers();
    app.Run();
}