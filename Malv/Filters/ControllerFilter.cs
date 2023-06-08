using System.Data.Common;
using System.Reflection;
using Malv.Controllers.Exceptions;
using Malv.Data.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Malv.Filters;

public class ControllerFilter : IActionFilter
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction _transaction;
    
    public ControllerFilter(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var attribute = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo
            .GetCustomAttributes<TransactionAttribute>().FirstOrDefault();

        if (attribute != null)
        {
            _transaction = _dbContext.Database.BeginTransaction();
            _dbContext.HasTransaction = true;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is MalvException malvException)
        {
            Console.WriteLine(string.Join(",", malvException.Error.Errors.Select(x => x.Key).ToArray()));
            context.Result = new BadRequestObjectResult(malvException.Error)
            {
                StatusCode = malvException.ExceptionToStatusCode()
            };
            context.ExceptionHandled = true;
        }

        if (_dbContext.HasTransaction)
        {
            if (context.Exception != null)
            {
                _transaction?.Rollback();
            }
            else
            {
                _transaction?.Commit();
            }
        }
    }
}