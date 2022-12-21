using RateLimiterTest.Helper;
using System;
using System.Collections.Concurrent;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace RateLimiterTest
{
    public class RateLimiterTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public RateLimiterTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ConcurrentRateLimitTest()
        {
            ConcurrentBag<string> concurrentLogs = new ConcurrentBag<string>();
            var res = Parallel.For(1, 30, async (i, state) =>
            {
                var result = await new HttpCalls().ConcurrentRateLimit(i,Guid.NewGuid().ToString());
                concurrentLogs.Add($"{i}. result: {result}");
            });

            Thread.Sleep(1000);
            foreach (var concurrentLog in concurrentLogs)
            {
                _testOutputHelper.WriteLine(concurrentLog);
            }

        }

        [Fact]
        public async Task FixedWindowTest()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = await new HttpCalls().FixedWindowLimit(i, Guid.NewGuid().ToString());
                _testOutputHelper.WriteLine($"{i}. result: {result}");
            }
        }

        [Fact]
        public async Task TokenBucketTest()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(200);
                var result = await new HttpCalls().TokenBucket(i,Guid.NewGuid().ToString());
                _testOutputHelper.WriteLine($"{i}. result: {result}");
            }
        }

        [Fact]
        public async Task SlidingWindowTest()
        {
            var res = Parallel.For(1, 200, async (i, state) =>
            {
                var result = await new HttpCalls().SlidingWindow(i,Guid.NewGuid().ToString());
                _testOutputHelper.WriteLine($"{i}. result: {result}");
            });
        }
    }
}