namespace MultiTenantTest.WebAPI.Services
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.Split('/');
            if (path.Length > 1)
            {
                context.Items["Tenant"] = path[1];
            }
            await _next(context);
        }
    }

}
