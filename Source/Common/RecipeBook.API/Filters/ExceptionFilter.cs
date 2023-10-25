using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeBook.Domain.Dtos.Responses;
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
            case ExceptionEmailSignUp:
                HandleEmailSignUpException(context);
                break;
            case ExceptionPhoneSignUp:
                HandlePhoneSignUpException(context);
                break;
        }
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionValidator;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(exception!.ErrorMessages!));
    }

    private static void HandleEmailSignUpException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.EMAIL_JA_REGISTRADO! }));
    }

    private static void HandlePhoneSignUpException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.TELEFONE_JA_REGISTRADO! }));
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ExceptionResponse(new List<string> { ErrorMessages.ERRO_DESCONHECIDO }));
    }
}