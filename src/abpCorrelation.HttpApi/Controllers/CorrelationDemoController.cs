using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;

namespace abpCorrelation.HttpApi.Controllers;

/// <summary>
/// Demo controller showing correlation ID usage
/// </summary>
[Route("api/correlation-demo")]
public class CorrelationDemoController : AbpControllerBase, ITransientDependency
{
    private readonly ILogger<CorrelationDemoController> _logger;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public CorrelationDemoController(
        ILogger<CorrelationDemoController> logger,
        ICorrelationIdProvider correlationIdProvider)
    {
        _logger = logger;
        _correlationIdProvider = correlationIdProvider;
    }

    /// <summary>
    /// Basic endpoint that shows correlation ID in logs
    /// </summary>
    [HttpGet("basic")]
    public IActionResult BasicDemo()
    {
        var correlationId = _correlationIdProvider.GetCorrelationId();
        
        _logger.LogInformation("Basic demo endpoint called with correlation ID: {CorrelationId}", correlationId);
        
        return Ok(new
        {
            Message = "Basic correlation ID demo",
            CorrelationId = correlationId,
            Timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Advanced endpoint showing correlation context creation
    /// </summary>
    [HttpGet("advanced")]
    public IActionResult AdvancedDemo()
    {
        var parentCorrelationId = _correlationIdProvider.GetCorrelationId();
        
        _logger.LogInformation("Advanced demo started with parent correlation ID: {ParentCorrelationId}", 
            parentCorrelationId);

        using (var context = _correlationIdProvider.CreateContext())
        {
            _logger.LogInformation("Created new correlation context: {CorrelationId}", context.CorrelationId);
            
            // Simulate some work
            Thread.Sleep(100);
            
            _logger.LogInformation("Work completed in correlation context: {CorrelationId}", context.CorrelationId);
            
            return Ok(new
            {
                Message = "Advanced correlation ID demo",
                ParentCorrelationId = parentCorrelationId,
                CurrentCorrelationId = context.CorrelationId,
                Metadata = context.Metadata,
                Timestamp = DateTime.UtcNow
            });
        }
    }

    /// <summary>
    /// Endpoint that demonstrates error logging with correlation ID
    /// </summary>
    [HttpGet("error-demo")]
    public IActionResult ErrorDemo()
    {
        var correlationId = _correlationIdProvider.GetCorrelationId();
        
        _logger.LogWarning("This is a warning message with correlation ID: {CorrelationId}", correlationId);
        
        try
        {
            // Simulate an error
            throw new InvalidOperationException("This is a simulated error for correlation ID demo");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred with correlation ID: {CorrelationId}", correlationId);
            
            return BadRequest(new
            {
                Message = "Error demo completed",
                CorrelationId = correlationId,
                Error = ex.Message,
                Timestamp = DateTime.UtcNow
            });
        }
    }

    /// <summary>
    /// Endpoint that shows correlation ID propagation in nested calls
    /// </summary>
    [HttpGet("nested")]
    public async Task<IActionResult> NestedDemo()
    {
        var rootCorrelationId = _correlationIdProvider.GetCorrelationId();
        
        _logger.LogInformation("Nested demo started with root correlation ID: {RootCorrelationId}", 
            rootCorrelationId);

        var results = new List<object>();

        // Simulate nested operations
        for (int i = 1; i <= 3; i++)
        {
            using (var context = _correlationIdProvider.CreateContext())
            {
                _logger.LogInformation("Nested operation {OperationNumber} with correlation ID: {CorrelationId}", 
                    i, context.CorrelationId);
                
                // Simulate async work
                await Task.Delay(50);
                
                results.Add(new
                {
                    OperationNumber = i,
                    CorrelationId = context.CorrelationId,
                    ParentCorrelationId = context.Metadata.ParentCorrelationId
                });
            }
        }

        _logger.LogInformation("Nested demo completed with root correlation ID: {RootCorrelationId}", 
            rootCorrelationId);

        return Ok(new
        {
            Message = "Nested correlation ID demo",
            RootCorrelationId = rootCorrelationId,
            Operations = results,
            Timestamp = DateTime.UtcNow
        });
    }
} 