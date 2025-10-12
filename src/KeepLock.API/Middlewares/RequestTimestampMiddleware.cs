namespace KeepLock.API.Middleware;

/// <summary>
/// Middleware to add request timestamp to context
/// </summary>
public class RequestTimestampMiddleware
{
    private readonly RequestDelegate _next;

    public RequestTimestampMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Add request timestamp to context
        context.Items["RequestTimestamp"] = DateTime.UtcNow.Ticks;

        // Add unique request ID if not existing
        if (!context.Items.ContainsKey("RequestId"))
        {
            context.Items["RequestId"] = Guid.NewGuid().ToString();
        }

        await _next(context);
    }
}

/// <summary>
/// Extension method to register middleware
/// </summary>
public static class RequestTimestampMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTimestamp(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestTimestampMiddleware>();
    }
}