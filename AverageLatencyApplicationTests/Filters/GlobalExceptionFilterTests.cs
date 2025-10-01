using AverageLatencyApplication.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;

namespace AverageLatencyApplicationTests.Filters;

public class GlobalExceptionFilterTests
{
    [Fact]
    public void OnException_WhenExceptionPresent_SetsProblemDetailsResponse()
    {
        // Arrange
        var filter = new GlobalExceptionFilter(NullLogger<GlobalExceptionFilter>.Instance);
        var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ControllerActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
        {
            Exception = new InvalidOperationException("Test exception")
        };

        // Act
        filter.OnException(exceptionContext);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(exceptionContext.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        var problemDetails = Assert.IsType<ProblemDetails>(objectResult.Value);
        Assert.Equal("An unexpected error occurred.", problemDetails.Title);
        Assert.Equal(StatusCodes.Status500InternalServerError, problemDetails.Status);
        Assert.Equal("Please try again later or contact support if the issue persists.", problemDetails.Detail);
        Assert.True(exceptionContext.ExceptionHandled);
    }

    [Fact]
    public void OnException_WhenExceptionMissing_DoesNotModifyContext()
    {
        // Arrange
        var filter = new GlobalExceptionFilter(NullLogger<GlobalExceptionFilter>.Instance);
        var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ControllerActionDescriptor());
        var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());

        // Act
        filter.OnException(exceptionContext);

        // Assert
        Assert.Null(exceptionContext.Result);
        Assert.False(exceptionContext.ExceptionHandled);
    }
}
