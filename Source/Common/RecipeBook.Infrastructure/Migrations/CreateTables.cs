using Dapper;
using MySqlConnector;

namespace RecipeBook.Infrastructure.Migrations;

public class CreateTables
{
    public static void CreateTableAccountAsync(string connectionString)
    {
        using var mysqlconnection = new MySqlConnection(connectionString);
        mysqlconnection.Open();
        mysqlconnection.Execute(
               $@"CREATE TABLE IF NOT EXISTS DB_Test.TB_Account (
                      Id CHAR(36) NOT NULL,
                      Name VARCHAR(255) NOT NULL,
                      Email VARCHAR(255) NOT NULL,
                      EmailConfirmed TINYINT(1) NOT NULL, 
                      Code VARCHAR(6) NULL,
                      Password VARCHAR(255) NOT NULL,
                      Phone VARCHAR(100) NOT NULL,
                      CreatedAt DATETIME NOT NULL,
                      UpdatedAt DATETIME NOT NULL,
                      PRIMARY KEY (Id));");
    }

    public static void CreateTableRecipeAsync(string connectionString)
    {
        using var mysqlconnection = new MySqlConnection(connectionString);
        mysqlconnection.Open();
        mysqlconnection.Execute(
            $@"CREATE TABLE IF NOT EXISTS DB_Test.TB_Recipe (
                  RecipeId CHAR(36) NOT NULL,
                  Title VARCHAR(255) NOT NULL,
                  Category INT NOT NULL,
                  MethodPreparation VARCHAR(5000) NOT NULL,
                  CreatedAt DATETIME NOT NULL,
                  UpdatedAt DATETIME NOT NULL,
                  PRIMARY KEY (RecipeId));");
    }

    public static void CreateTableIngredientAsync(string connectionString)
    {
        using var mysqlconnection = new MySqlConnection(connectionString);
        mysqlconnection.Open();
        mysqlconnection.Execute(
            $@"CREATE TABLE IF NOT EXISTS DB_Test.TB_Ingredient (
                  IngredientId CHAR(36) NOT NULL,
                  Product VARCHAR(255) NOT NULL,
                  Quantity INT NOT NULL,
                  CreatedAt DATETIME NOT NULL,
                  UpdatedAt DATETIME NOT NULL,
                  RecipeId CHAR(36) NOT NULL,
                  PRIMARY KEY (IngredientId),
                  FOREIGN KEY (RecipeId) REFERENCES DB_Test.TB_Recipe(RecipeId));");
    }
}