namespace gameleaderboardapi.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string API_KEY_HEADER = "Leaderboard-Api-Key";
    //must have, _next is the gate to the next middleware
    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        //if the required header doesn't exist
        if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var apiKeySent))
        {
            //make 401 response
            context.Response.StatusCode = 401; // 401 = Unauthorized
            await context.Response.WriteAsJsonAsync(new {Message = "Error: Missing Leaderboard API Key"});
            return;
        }
        var validApiKey = config["GameSecrets:LeaderboardApiKey"];
        //someone tried to send a false api key
        if (validApiKey != apiKeySent)
        {
            context.Response.StatusCode = 403; // 403 = Forbidden
            await context.Response.WriteAsJsonAsync(new {Message = "API Key Invalid"});
            return;
        }

        await _next(context);
    }

}
