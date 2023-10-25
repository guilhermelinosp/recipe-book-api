﻿namespace RecipeBook.Domain.Dtos.Responses;

public class ExceptionResponse
{
    public List<string>? Mensagens { get; set; }

    public ExceptionResponse(List<string>? mensagens)
    {
        Mensagens = mensagens;
    }
}