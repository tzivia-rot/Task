using Tasks.Interfaces;

namespace Tasks.Middleware;

public class LogMiddleware
{
    private readonly ILog logger;
    private readonly RequestDelegate next;
     public LogMiddleware(RequestDelegate next, ILog logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        logger.Log(LogLevel.Debug, $"start {ctx.Request.Path}");
        await next(ctx);
        logger.Log(LogLevel.Debug, $"end {ctx.Request.Path}");
    }
}