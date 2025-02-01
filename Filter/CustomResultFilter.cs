using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using workingProject.EfDbContext.DBUnitOfWork;

namespace workingProject.Filter
{
    public class CustomResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext resultContext, ResultExecutionDelegate next)
        {
            if (resultContext.Result is OkObjectResult okResult)
            {
                var value = okResult.Value;

                // If value is not null, wrap it in ApiResponse
                if (value != null && value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    return; // If it's already wrapped, do nothing
                }

                var apiResponse = new ApiResponse<object>(value);
                resultContext.Result = new OkObjectResult(apiResponse); // Wrap the value in ApiResponse and set the result
            }
            // If it's not an OkObjectResult, you can handle other cases, e.g., NotFound or BadRequest.
            else if (resultContext.Result is ObjectResult objectResult)
            {
                // Wrap all other ObjectResult (e.g., NotFound, BadRequest) into ApiResponse as well
                var apiResponse = new ApiResponse<object>(objectResult.Value);
                resultContext.Result = new ObjectResult(apiResponse)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
            await next();
        }
    }
}
