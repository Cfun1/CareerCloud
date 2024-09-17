using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CareerCloud.WebApp;
//Not used: check program.cs
internal class IgnoreTimeStampFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Remove TimeStamp from model state validation

        var timestmp = context.ModelState.Keys.FirstOrDefault(e => e.Contains(nameof(IRowVersion.TimeStamp)));
        if (timestmp != null)
            context.ModelState.Remove(timestmp);

        base.OnActionExecuting(context); // Ensure the base method is called
    }
}