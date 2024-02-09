using Microsoft.Extensions.Caching.Memory;
namespace APIRateLimiting.MiddleWares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public RateLimitMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string ipAddress = context.Connection.RemoteIpAddress.ToString();

            string cacheKey = ipAddress;

            if (!_cache.TryGetValue(cacheKey, out int requestCount))
            {
                requestCount = 0;
            }

            // Increment request count
            requestCount++;
            _cache.Set(cacheKey, requestCount, TimeSpan.FromMinutes(1));

            int maxRequests = 3;

            if (requestCount > maxRequests)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.Headers.Add("Retry-After", "60");
                await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                return;
            }

            await _next(context);
        }
    }
}