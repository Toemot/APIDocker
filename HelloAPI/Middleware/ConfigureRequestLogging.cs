using Serilog;
using Serilog.Events;

namespace HelloAPI.Middleware
{
    public static class ConfigureRequestLogging
    {
        public static IApplicationBuilder UseCustomRequestLogging(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = ExcludeHealthChecks;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"]);
                    diagnosticContext.Set("Name", httpContext.User.Identity);
                };
            });
        }

        private static LogEventLevel ExcludeHealthChecks(HttpContext ctx, double arg2, Exception ex) =>
            ex != null ? LogEventLevel.Error : ctx.Response.StatusCode > 499
                ? LogEventLevel.Error : IsHealthCheckEndpoint(ctx)
                ? LogEventLevel.Verbose : LogEventLevel.Information;

        private static bool IsHealthCheckEndpoint(HttpContext ctx)
        {
            var userAgent = ctx.Request.Headers["User-Agent"].FirstOrDefault() ?? "";
            return ctx.Request.Path.Value.EndsWith("health", StringComparison.CurrentCultureIgnoreCase) || 
                userAgent.Contains("HealthCheck", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
