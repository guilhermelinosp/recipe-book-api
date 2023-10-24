﻿using System.Runtime.Serialization;

namespace RecipeBook.Exceptions.Exceptions;

[Serializable]
public class ValidatorException : BaseException
{
    public List<string>? ErrorMessages { get; set; }

    public ValidatorException(List<string>? errorMessages) : base(string.Empty)
    {
        ErrorMessages = errorMessages;
    }

    protected ValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
