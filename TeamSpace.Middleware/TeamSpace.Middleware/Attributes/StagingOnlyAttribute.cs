using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TeamSpace.Middleware.Attributes;

public class StagingOnlyAttribute : Attribute, IResourceFilter
{
    public StagingOnlyAttribute()
    {
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        var env = context.HttpContext.RequestServices.GetService<IWebHostEnvironment>();
        if (env.IsProduction())
        {
            context.Result = new NotFoundResult();
        }
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
    }
}