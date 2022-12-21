// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;
using RateLimiterConsole;
using System.Net.Http.Headers;

#region ConcurrentRateLimit
//var res = Parallel.For(1, 10, (i, state) =>
//{
//    Task.Run(async () =>
//    {
//        await new HttpCalls().ConcurrentRateLimit(Guid.NewGuid().ToString());
//    });
//});
#endregion

#region Fixed Window
//for (int i = 0; i < 100; i++)
//{
//    await new HttpCalls().FixedWindowLimit(i, Guid.NewGuid().ToString());
//}
#endregion


#region TokenBucket
//int i = 0;
//var resultTokenBucket = Parallel.For(1, 30, (i, state) =>
//{
//    i++;
//    Task.Run(async () =>
//    {
//        await new HttpCalls().TokenBucket(Guid.NewGuid().ToString());
//    });
//}); 
#endregion

//int i = 0;
//var res = Parallel.For(1, 100, (i, state) =>
//{
//    i++;
//    if (i % 20 == 0)
//    {
//        Thread.Sleep(1000);
//    }
//    Task.Run(async () =>
//    {
//        await new HttpCalls().SlidingWindow(Guid.NewGuid().ToString());
//    });
//});

//for (int i = 0; i < 100; i++)
//{
//    if (i % 20 == 0)
//    {
//        Thread.Sleep(1000);
//    }
//    Task.Run(async () =>
//    {
//        await new HttpCalls().SlidingWindow(Guid.NewGuid().ToString());
//    });
//}





Console.ReadLine();