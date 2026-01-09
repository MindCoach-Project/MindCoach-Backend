using MinhCoach.Api;
using MinhCoach.Api.Hubs;
using MinhCoach.App;
using MinhCoach.Infra;
using MinhCoach.Infra.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()   
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
    }
    app.UseHttpsRedirection(); 
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowAll");
    
    app.MapControllers();
    
    app.MapHub<ReminderHub>("/reminderHub");
    
    app.Run();
}