using TravelMeNow.DataAccess;
using TravelMeNow.Application;
using Microsoft.AspNetCore.Server.Kestrel.Core;


var MyAllowAnyOrigin = "_myAllowAnyOrigin";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenAnyIP(5217, listenOptions =>
    {
        listenOptions.UseHttps("./final_certificate.pfx", "");
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAnyOrigin,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(MyAllowAnyOrigin);

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

