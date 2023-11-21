using Dapper;
using MySqlConnector;

namespace RecipeBook.Infrastructure.Migrations;

public static class CreateTables
{
    public static async Task CreateTableAccountAsync(string connectionString)
    {
        await using var mySqlConnection = new MySqlConnection(connectionString);
        await mySqlConnection.OpenAsync();
        await mySqlConnection.ExecuteAsync(
            """
            CREATE TABLE IF NOT EXISTS DB_Test.TB_Account (
                                  AccountId CHAR(36) NOT NULL,
                                  Name VARCHAR(255) NOT NULL,
                                  Email VARCHAR(255) NOT NULL,
                                  EmailConfirmed TINYINT(1) NOT NULL,
                                  Code VARCHAR(6) NULL,
                                  Password VARCHAR(255) NOT NULL,
                                  Phone VARCHAR(100) NOT NULL,
                                  CreatedAt DATETIME NOT NULL,
                                  UpdatedAt DATETIME NOT NULL,
                                  PRIMARY KEY (AccountId))
            """);
    }

    public static async Task  CreateTableRecipeAsync(string connectionString)
    {
        await using var mySqlConnection = new MySqlConnection(connectionString);
        await mySqlConnection.OpenAsync();
        await mySqlConnection.ExecuteAsync(
            """
            CREATE TABLE IF NOT EXISTS DB_Test.TB_Recipe (
                   RecipeId CHAR(36) NOT NULL,
                   Title VARCHAR(255) NOT NULL,
                   Category INT NOT NULL,
                   PreparationMode VARCHAR(5000) NOT NULL,
                   PreparationTime INT NOT NULL,
                   CreatedAt DATETIME NOT NULL,
                   UpdatedAt DATETIME NOT NULL,
                   AccountId CHAR(36) NOT NULL,
                   PRIMARY KEY (RecipeId),
                   FOREIGN KEY (AccountId) REFERENCES DB_Test.TB_Account(AccountId))
            """);
    }

    public static async Task  CreateTableIngredientAsync(string connectionString)
    {
        await using var mySqlConnection = new MySqlConnection(connectionString);
        await mySqlConnection.OpenAsync();
        await mySqlConnection.ExecuteAsync(
            """
            CREATE TABLE IF NOT EXISTS DB_Test.TB_Ingredient (
                              IngredientId CHAR(36) NOT NULL,
                              Product VARCHAR(255) NOT NULL,
                              Quantity VARCHAR(255) NOT NULL,
                              CreatedAt DATETIME NOT NULL,
                              UpdatedAt DATETIME NOT NULL,
                              RecipeId CHAR(36) NOT NULL,
                              PRIMARY KEY (IngredientId),
                              FOREIGN KEY (RecipeId) REFERENCES DB_Test.TB_Recipe(RecipeId))
            """);
    }
}