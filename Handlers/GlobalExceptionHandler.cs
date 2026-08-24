
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace gameleaderboardapi.Handlers;

public class GlobalExceptionHandler: IExceptionHandler
{
    //need logger (report to server), and IProblemDetailsService to create a standard response (NEVER SERIALIZE JSON)
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;
    // DI
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
    {
        _logger = logger; //auto injected, no additional lines needed in program.cs
        _problemDetailsService = problemDetailsService; //get injected with app.AddProblemDetails()
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken c_token)
    {
        _logger.LogError(ex, "Loi he thong: {Message}", ex.Message); // send this to server, full message
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError, // Can change but global exception should mostly be 500
            Title = "Loi he thong nghiem trong",
            Type = ex.GetType().Name,
            Detail = "Da xay ra su co khong mong muon. Vui long thu lai sau" // NEVER USE ex.Message, it can leak info
        };
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = problemDetails
        });
    }

}