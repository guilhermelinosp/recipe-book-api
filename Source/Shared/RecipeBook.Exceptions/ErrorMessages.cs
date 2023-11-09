namespace RecipeBook.Exceptions;

public class ErrorMessages
{
    public static string CATEGORIA_RECEITA_INVALIDA => "The recipe category is invalid.";
    public static string CODIGO_NAO_ENCONTRADO => "Code not found.";
    public static string EMAIL_JA_REGISTRADO => "The  user's email provided is already registered.";
    public static string EMAIL_USUARIO_EMBRANCO => "The user's email must be provided.";
    public static string EMAIL_USUARIO_INVALIDO => "The user's email is invalid.";
    public static string ERRO_DESCONHECIDO => "Unknown error.";
    public static string LOGIN_INVALIDO => "The  user's email and/or password are incorrect.";
    public static string MODOPREPARO_RECEITA_EMBRANCO => "The recipe preparation method must be informed.";
    public static string NOME_USUARIO_EMBRANCO => "The  user's name must be provided.";
    public static string RECEITA_INGREDIENTE_PRODUTO_EMBRANCO => "The ingredient product must be informed.";
    public static string RECEITA_INGREDIENTE_QUANTIDADE_EMBRANCO => "The quantity of the ingredient must be informed.";
    public static string RECEITA_INGREDIENTES_REPETIDOS => "There are repeated ingredients in your list.";
    public static string RECEITA_MINIMO_UM_INGREDIENTE => "The recipe must have at least one ingredient.";
    public static string RECEITA_NAO_ENCONTRADA => "Recipe not found.";
    public static string SENHA_ATUAL_INVALIDA => "Current password is invalid.";
    public static string SENHA_USUARIO_EMBRANCO => "The user's password must be entered.";
    public static string SENHA_USUARIO_MINIMO_OITO_CARACTERES => "The user's password must contain at least 8 characters.";
    public static string SENHA_USUARIO_INVALIDA => "The user's password is invalid.";
    public static string TELEFONE_USUARIO_EMBRANCO => "The user's phone number must be provided.";
    public static string TELEFONE_USUARIO_INVALIDO => "The user's phone number must be in the format XXXXXXXXXXX";
    public static string TEMPO_PREPARO_INVALIDO => "The preparation time is invalid.";
    public static string TITULO_RECEITA_EMBRANCO => "The title of the recipe must be provided.";
    public static string TOKEN_EXPIRADO => "The token has expired";
    public static string USUARIO_NAO_ENCONTRADO => "The account not found";
    public static string USUARIO_SEM_PERMISSAO => "You do not have permission to access this resource.";
    public static string VOCE_NAO_PODE_EXECUTAR_ESTA_OPERACAO => "You cannot perform this operation.";
    public static string TELEFONE_JA_REGISTRADO => "The user's phone number has already been registered.";
    public static string NOME_USUARIO_INVALIDO => "The user's name is invalid.";
    public static string SENHA_USUARIO_MAXIMO_DEZESSEIS_CARACTERES => "The user's password must contain a maximum of 16 characters.";
    public static string TOKEN_INVALIDO => "The token is invalid";
    public static string EMAIL_NAO_CONFIRMADO => "Email not confirmed";
    public static string CODIGO_INVALIDO => "The code is invalid";
}