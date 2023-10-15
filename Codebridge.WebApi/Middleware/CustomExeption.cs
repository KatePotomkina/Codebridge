public class CustomExeption:Exception
{
    private readonly RequestDelegate _next;

    public CustomExeption(RequestDelegate next)
    {
        _next = next;
    }
    public CustomExeption(string name)  : base($"Dog with name {name} already exists")
    {
    }

    public CustomExeption(string message, Exception innerException) : base(message, innerException)
    {
    }
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try 
            {
                await _next(context);
            }
            catch (CustomExeption ex)
            {
                context.Response.StatusCode = 409; // Conflict
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("Unexpected error");
            }
        }
    }
}