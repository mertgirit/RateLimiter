using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region RateLimiter Implementation

app.UseRateLimiter(
    new RateLimiterOptions()
    {
        OnRejected = (context, ct) =>
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            return new ValueTask();
        },
        RejectionStatusCode = StatusCodes.Status429TooManyRequests
    }
    .AddConcurrencyLimiter("concurrency", c =>
     {
         c.PermitLimit = 5; //concurrent request limit 1 ile sýnýrlý.
         c.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; //FIFO mantýðýnda requestler iþleniyor.
     })
    .AddFixedWindowLimiter("window", w =>
    {
        w.QueueLimit = 20; //window doluyken queue ya alabileceðimiz request sayýsý eðer queuelimit yoksa  gelen requestler block edilir. Eðer queueya alýrsak yeni windowa kadar bekletir.
        w.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        w.AutoReplenishment = true;
        w.Window = new TimeSpan(0, 0, 5); //windowun yenilenme süresi
        w.PermitLimit = 20; //tek windowda iþleyebileceðimiz request sayýsý.
    })
    .AddTokenBucketLimiter("tokenbucket", tb =>
    {
        tb.TokenLimit = 5; //sepetteki token limitim 5.
        tb.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        tb.TokensPerPeriod = 1; // her yenilemede 1 token ekle.
        tb.ReplenishmentPeriod = new TimeSpan(0, 0, 5); //her 5 saniyede bir yenile
    })
    .AddSlidingWindowLimiter("slidingwindow", sw =>
    {
        sw.Window = new TimeSpan(0, 0, 5);
        sw.PermitLimit = 30;
        sw.QueueLimit = 5;
        sw.SegmentsPerWindow = 2;
        sw.AutoReplenishment = true;
        sw.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    })
    );

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
