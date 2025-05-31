namespace API.Web.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var response = context.Response;
            response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case ForbiddenException:
                    response.StatusCode = StatusCodes.Status403Forbidden;
                    break;
                case BadRequestException:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
        }
    }
}