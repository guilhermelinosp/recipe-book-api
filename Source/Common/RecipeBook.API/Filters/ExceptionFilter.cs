using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;
using System.Net;

namespace RecipeBook.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ExceptionBase)
        {
            HandleException(context);
        }
        else
        {
            HandleExceptionUnknown(context);
        }
    }

    private static void HandleException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ExceptionValidator:
                HandleExceptionValidation(context);
                break;
            case ExceptionSignUp:
                HandleExceptionSignUp(context);
                break;
            case ExceptionSignIn:
                HandleExceptionSignIn(context);
                break;
        }
    }

    private static void HandleExceptionValidation(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionValidator;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleExceptionSignUp(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionSignUp;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleExceptionUnknown(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.ERRO_DESCONHECIDO }));
    }
    private static void HandleExceptionSignIn(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.LOGIN_INVALIDO }));
    }
}