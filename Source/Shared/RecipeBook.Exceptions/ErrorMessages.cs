namespace RecipeBook.Exceptions;

public class ErrorMessages
{
    // Account
    // Name
    public static string NOME_USUARIO_INVALIDO => "The user's name is invalid.";
    public static string NOME_USUARIO_NAO_INFORMADO => "The  user's name must be provided.";

    // Email
    public static string EMAIL_USUARIO_INVALIDO => "The user's email is invalid.";
    public static string EMAIL_USUARIO_NAO_INFORMADO => "The user's email must be provided.";
    public static string EMAIL_USUARIO_NAO_CONFIRMADO => "The user's email not confirmed";
    public static string EMAIL_USUARIO_JA_REGISTRADO => "The  user's email provided is already registered.";
    public static string EMAIL_USUARIO_NAO_ENCONTRADO => "The user's email not found";
    public static string EMAIL_USUARIO_CODIGO_INVALIDO => "The email's code is invalid";


    // Password
    public static string SENHA_USUARIO_INVALIDA => "The user's password is invalid.";
    public static string SENHA_USUARIO_NAO_INFORMADO => "The user's password must be entered.";
    public static string SENHA_USUARIO_MINIMO_OITO_CARACTERES => "The user's password must contain at least 8 characters.";
    public static string SENHA_USUARIO_MAXIMO_DEZESSEIS_CARACTERES => "The user's password must contain a maximum of 16 characters.";

    // Phone
    public static string TELEFONE_USUARIO_INVALIDO => "The user's phone number must be in the format XXXXXXXXXXX";
    public static string TELEFONE_USUARIO_NAO_INFORMADO => "The user's phone number must be provided.";
    public static string TELEFONE_USUARIO_JA_REGISTRADO => "The user's phone number has already been registered.";
    public static string TELEFONE_USUARIO_NAO_CONFIRMADO => "The user's email not confirmed";
    public static string TELEFONE_USUARIO_CODIGO_INVALIDO => "The phone's code is invalid";


    // Token
    public static string TOKEN_INVALIDO => "The token is invalid.";
    public static string TOKEN_NAO_INFORMADO => "The token must be informed.";
    public static string TOKEN_EXPIRADO => "The token has expired.";
    public static string TOKEN_SEM_PERMISSAO => "You do not have permission to access this resource.";

    // Ingredient
    public static string INGREDIENTE_NAO_ENCONTRADO => "Ingredient not found.";


    public static string INGREDIENTE_PRODUTO_INVALIDO => "The ingredient product is invalid.";
    public static string INGREDIENTE_PRODUTO_NAO_INFORMADO => "The ingredient product must be informed.";

    public static string INGREDIENTE_PRODUTO_JA_CADASTRADO => "The ingredient product has already been registered.";
    public static string INGREDIENTE_PRODUTO_NAO_ENCONTRADO => "The ingredient product not found.";


    public static string INGREDIENTE_QUANTIDADE_INVALIDA => "The ingredient quantity is invalid.";
    public static string INGREDIENTE_QUANTIDADE_NAO_INFORMADA => "The ingredient quantity must be informed.";


    public static string INGREDIENTE_NAO_PODE_SER_REMOVIDO => "The ingredient cannot be removed because it is being used in a recipe.";
    public static string INGREDIENTE_NAO_PODE_SER_ATUALIZADO => "The ingredient cannot be updated because it is being used in a recipe.";


    // Recipe
    public static string RECEITA_NAO_PODE_SER_REMOVIDO => "The recipe cannot be removed because it is being used in a recipe.";
    public static string RECEITA_NAO_PODE_SER_ATUALIZADO => "The recipe cannot be updated because it is being used in a recipe.";


    // Recipe Name
    public static string RECEITA_TITULO_INVALIDO => "The recipe title is invalid.";
    public static string RECEITA_TITULO_NAO_INFORMADO => "The recipe title must be informed.";
    public static string RECEITA_TITULO_JA_CADASTRADO => "The recipe title has already been registered.";
    public static string RECEITA_TITULO_NAO_ENCONTRADO => "The recipe title not found.";


    // Recipe PreparationTime
    public static string RECEITA_TEMPOPREPARO_INVALIDO => "The preparation time is invalid.";
    public static string RECEITA_TEMPOPREPARO_NAO_INFORMADO => "The preparation time must be informed.";


    // Recipe PreparationMode
    public static string RECEITA_MODOPREPARO_RECEITA_INVALIDO => "The recipe preparation method is invalid.";
    public static string RECEITA_MODOPREPARO_RECEITA_NAO_INFORMADO => "The recipe preparation method must be informed.";


    // Recipe Categoty
    public static string RECEITA_CATEGORIA_INVALIDO => "The recipe category is invalid.";
    public static string RECEITA_CATEGORIA_NAO_INFORMADO => "The recipe category must be informed.";
    public static string RECEITA_CATEGORIA_NAO_ENCONTRADO => "The recipe category not found.";


    // Recipe Ingredients
    public static string RECEITA_INGREDIENTE_NAO_INFORMADO => "The ingredient must be informed.";
    public static string RECEITA_INGREDIENTES_REPETIDOS => "There are repeated ingredients in your list.";


    // Recipe Ingredients Product
    public static string RECEITA_INGREDIENTE_PRODUTO_INVALIDO => "The ingredient product is invalid.";
    public static string RECEITA_INGREDIENTE_PRODUTO_NAO_INFORMADO => "The ingredient product must be informed.";


    // Recipe Ingredients Quantity
    public static string RECEITA_INGREDIENTE_QUANTIDADE_INVALIDA => "The quantity of the ingredient is invalid.";
    public static string RECEITA_INGREDIENTE_QUANTIDADE_NAO_INFORMADO => "The quantity of the ingredient must be informed.";
    public static string RECEITA_MINIMO_UM_INGREDIENTE => "The recipe must have at least one ingredient.";


    // Others
    public static string ERRO_DESCONHECIDO => "Unknown error.";
    public static string ERRO_AO_CRIAR_USUARIO => "Error creating user.";
    public static string ERRO_AO_ATUALIZAR_USUARIO => "Error updating user.";
    public static string ERRO_AO_DELETAR_USUARIO => "Error deleting user.";
    public static string ERRO_AO_CRIAR_RECEITA => "Error creating recipe.";
    public static string ERRO_AO_ATUALIZAR_RECEITA => "Error updating recipe.";
    public static string ERRO_AO_DELETAR_RECEITA => "Error deleting recipe.";
    public static string ERRO_AO_CRIAR_INGREDIENTE => "Error creating ingredient.";
    public static string ERRO_AO_ATUALIZAR_INGREDIENTE => "Error updating ingredient.";
    public static string ERRO_AO_DELETAR_INGREDIENTE => "Error deleting ingredient.";

}