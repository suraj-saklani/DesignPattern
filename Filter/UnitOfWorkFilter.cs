using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using workingProject.EfDbContext.DBUnitOfWork;
using workingProject.WPAttribute;

namespace workingProject.Filter
{
    public class UnitOfWorkFilter : IAsyncActionFilter, IAsyncExceptionFilter
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork=unitOfWork;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var disableUnitOfWorkAttribute = context.ActionDescriptor.EndpointMetadata
            .OfType<DisableUnitOfWorkAttribute>()
            .FirstOrDefault();
            if (disableUnitOfWorkAttribute!=null)
            {
                await next();
                return;

            }

            await unitOfWork.BeginTransactionAsync();
            var resultContext = await next();
            if(resultContext.Exception == null)
            {
                await unitOfWork.CommitAsync();
            }
        }


        public async Task OnExceptionAsync(ExceptionContext context)
        {
            unitOfWork.Rollback();
            context.ExceptionHandled = false;
            var apiResponse = new ApiResponse<object>(context.Exception.Message);
            context.Result = new ObjectResult(apiResponse)
            {
                StatusCode = 500
            };
        }

        
    }
}
