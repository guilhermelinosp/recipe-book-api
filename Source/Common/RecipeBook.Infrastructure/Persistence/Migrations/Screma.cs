﻿using Dapper;
using MySqlConnector;

namespace RecipeBook.Infrastructure.Persistence.Migrations;

public static class Screma
{
    public static async Task CreateScremaAsync(string? connectionString, string? database)
    {
        await using var mysqlconnection = new MySqlConnection(connectionString);
        await mysqlconnection.OpenAsync();
        await mysqlconnection.ExecuteAsync($"CREATE DATABASE IF NOT EXISTS {database}");
    }

    public static async Task DropScremaAsync(string connectionString, string database)
    {
        await using var mysqlconnection = new MySqlConnection(connectionString);
        await mysqlconnection.OpenAsync();
        await mysqlconnection.ExecuteAsync($"DROP DATABASE IF EXISTS {database}");
    }

    public static async Task CreateTablesAsync(string? connectionString, string? database)
    {
        var query = $@"CREATE TABLE IF NOT EXISTS {database}.TB_User (
                      Id INT NOT NULL AUTO_INCREMENT,
                      Name VARCHAR(100) NOT NULL,
                      Email VARCHAR(100) NOT NULL,
                      Password VARCHAR(100) NOT NULL,
                      Phone VARCHAR(100) NOT NULL,
                      CreatedAt DATETIME NOT NULL,
                      UpdatedAt DATETIME NOT NULL,
                      PRIMARY KEY (Id));";

        await using var mysqlconnection = new MySqlConnection(connectionString);
        await mysqlconnection.OpenAsync();
        await mysqlconnection.ExecuteAsync(query);
    }

    public static async Task DropTablesAsync(string connectionString, string database)
    {
        await using var mysqlconnection = new MySqlConnection(connectionString);
        await mysqlconnection.OpenAsync();
        await mysqlconnection.ExecuteAsync($"DROP TABLE IF EXISTS {database}.TB_User;");
    }
}