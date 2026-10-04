namespace CW_2.Middleware
{
    public class DocAccessMiddleware
    {
        private static readonly string[] _prefixes = new[]
        {
            "/swagger",
            "/scalar",
            "/openapi"
        };
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public DocAccessMiddleware(RequestDelegate next, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _next = next;
            _configuration = configuration;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;
            var isProtectedPath = _prefixes.Any(
                prefix => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));

            if (!isProtectedPath || _environment.IsDevelopment())
            {
                await _next(context);   // не документація, або ми в Development - пропускаємо
                return;
            }

            var expectedKey = _configuration["Docs:AccessKey"];

            if (string.IsNullOrEmpty(expectedKey))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;  // ключ не налаштовано - блокуємо
                return;
            }

            var providedKey = context.Request.Headers["X-Docs-Key"].FirstOrDefault()
                ?? context.Request.Query["docsKey"].FirstOrDefault();

            if (!string.Equals(providedKey, expectedKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;  // невірний ключ
                return;
            }

            await _next(context);
        }
    }
}
