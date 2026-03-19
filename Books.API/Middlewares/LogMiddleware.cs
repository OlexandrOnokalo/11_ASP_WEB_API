using System.Diagnostics;

namespace Books.API.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Request
            var requestHeaders = new List<string>();
            foreach (var header in context.Request.Headers)
            {
                requestHeaders.Add($"{header.Key} - {header.Value}");
            }

            _logger.LogInformation("REQUEST START");
            _logger.LogInformation($"Method: {context.Request.Method}\n" +
                $"Path: {context.Request.Path}\n" +
                $"Query: {context.Request.QueryString}\n" +
                $"{string.Join('\n', requestHeaders)}");

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Response
                var responseHeaders = new List<string>();
                foreach (var header in context.Response.Headers)
                {
                    responseHeaders.Add($"{header.Key} - {header.Value}");
                }

                _logger.LogInformation("RESPONSE END");
                _logger.LogInformation($"Status code: {context.Response.StatusCode}\n" +
                    $"Elapsed: {stopwatch.ElapsedMilliseconds} ms\n" +
                    $"{string.Join('\n', responseHeaders)}");
            }
        }
    }
}