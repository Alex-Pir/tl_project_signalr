using PmsAgentManager.HttpApi;
using PmsAgentManager.Hubs;
using PmsAgentManager.Services;

var builder = WebApplication.CreateBuilder(args);

var url = builder.Configuration["RemoteService:Url"];
var page = builder.Configuration["RemoteService:Page"];

if (url == null || page == null)
{
    throw new Exception("Couldn't find settings required settings for remote service");
}

builder.Services.AddSingleton<IHttpApi>(new HttpNpbApi(url, page));
builder.Services.AddSingleton<IRegistry, Registry>();
builder.Services.AddSingleton<IConnectionMapping<string>, ConnectionMapping>();

builder.Services.AddControllers();

builder.Services.AddSignalR(hubOption =>
{
    hubOption.EnableDetailedErrors = true;
    hubOption.ClientTimeoutInterval = TimeSpan.FromMinutes(6);
    hubOption.KeepAliveInterval = TimeSpan.FromMinutes(3);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.MapHub<AgentHub>("/signalr");

app.Run();