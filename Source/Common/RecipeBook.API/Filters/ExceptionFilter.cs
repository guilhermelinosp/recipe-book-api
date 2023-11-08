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
            HandleUnknownException(context);
        }
    }

    private static void HandleException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ExceptionValidator:
                HandleValidationException(context);
                break;
            case ExceptionSignUp:
                HandleSignUpException(context);
                break;
            case ExceptionSignIn:
                HandleSignInException(context);
                break;
            case ExceptionToken:
                HandleTokenException(context);
                break;
        }
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionValidator;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleSignUpException(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionSignUp;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        Console.WriteLine(context.Exception);
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.ERRO_DESCONHECIDO }));
    }
    private static void HandleSignInException(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionSignIn;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleTokenException(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionToken;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }
}