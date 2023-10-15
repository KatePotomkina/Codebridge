using AspNetCoreRateLimit;
using Codebridge.Codebridge.BLL.Repository;
using Codebridge.DbContext;
using Codebridge.Mapping;
using Codebridge.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DogDbConnection");
var ratelimiting = builder.Configuration.GetSection("IpRateLimiting");
// Add services to the container.

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddDbContext<DogDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IDogService, DogService>();
builder.Services.AddTransient<IDogRepository, DogRepository>();
builder.Services.AddTransient<IPingService, PingService>();

builder.Services.AddAutoMapper(typeof(DogMappingProfile)); 


builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "/dogs",
            Period = "1s",
            Limit = 1
        }
    };

});
builder.Services.AddOptions();
builder.Services.Configure<IpRateLimitOptions>(ratelimiting);
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();



var app = builder.Build();


/*
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();


app.MapControllers();
app.UseMiddleware<CustomExeption.ErrorHandlingMiddleware>();

app.Run();