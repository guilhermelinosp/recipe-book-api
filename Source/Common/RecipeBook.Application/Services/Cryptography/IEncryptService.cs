﻿namespace RecipeBook.Application.Services.Cryptography;

public interface IEncryptService
{
    string EncryptPassword(string password);

    string GenerateCode();
}