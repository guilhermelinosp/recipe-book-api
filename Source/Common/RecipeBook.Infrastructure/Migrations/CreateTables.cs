using Dapper;
using MySqlConnector;

namespace RecipeBook.Infrastructure.Migrations;

public class CreateTables
{
    public static void CreateTableAccountAsync(string? connectionString)
    {
        using var mysqlconnection = new MySqlConnection(connectionString);
        mysqlconnection.Open();
        mysqlconnection.Execute(
               @"CREATE TABLE IF NOT EXISTS DB_Test.TB_Account (
                      Id CHAR(36) NOT NULL,
                      Name VARCHAR(100) NOT NULL,
                      Email VARCHAR(100) NOT NULL,
                      Password VARCHAR(255) NOT NULL,
                      Phone VARCHAR(100) NOT NULL,
                      CreatedAt DATETIME NOT NULL,
                      UpdatedAt DATETIME NOT NULL,
                      PRIMARY KEY (Id));");
    }
}