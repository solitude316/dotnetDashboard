using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Dashboard.Extensions;

public class Auth : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Log("OnActionExecuting", context.RouteData);
    }

    public override void OnActionExecuted(ActionExecutedContext context) 
    {
        Log("OnActionExecuted", context.RouteData);
    }

    public override void OnResultExecuting(ResultExecutingContext context) 
    {
        Log("OnResultExecuting", context.RouteData);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Log("OnResultExecuted", context.RouteData);
    }

    private string Log(string methodName, RouteData routeData)
    {
        var controllerName = routeData.Values["controller"]?.ToString();
        var actionName = routeData.Values["action"]?.ToString();

        var log = $"{methodName}: {controllerName}/{actionName}";
        Debug.WriteLine(log);
        return log;
    }
}