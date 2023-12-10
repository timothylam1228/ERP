using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Data;
public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
	    _connectionString = connectionString;
    }

    public void InitializeDatabase()
    {
        // Create DB if it doesn't exist
        // CreateDatabaseIfNotExists();

        // Create tables
        // CreateTablesIfNotExists();
    }

    private void CreateDatabaseIfNotExists()
    {
        // Connection string for master database
		
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var dbName = "QBuild";
            var createDbCommand =
                $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{dbName}') CREATE DATABASE [{dbName}]";

            using (var command = new SqlCommand(createDbCommand, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    private void CheckAndCreateTable(string tableName, string createTableScript)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var checkTableExistsCommand = connection.CreateCommand();
            checkTableExistsCommand.CommandText = 
                $"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}') BEGIN {createTableScript} END";

            checkTableExistsCommand.ExecuteNonQuery();
        }
    }

    private void CreateTablesIfNotExists()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var createBomTable = @"
				CREATE TABLE [Bom] 
                    (
                        [PARENT_NAME]	VARCHAR(512),
                        [QUANTITY]	VARCHAR(512),
                        [COMPONENT_NAME]	VARCHAR(512)
                    );";

            var createPartTable = @"
				CREATE TABLE Part 
				(
				    NAME	VARCHAR(512),
				    TYPE	VARCHAR(512),
				    ITEM	VARCHAR(512),
				    PART_NUMBER	VARCHAR(512),
				    TITLE	VARCHAR(512),
				    MATERIAL	VARCHAR(512),
				);";
            
			CheckAndCreateTable("Bom", createBomTable);
			CheckAndCreateTable("Part", createPartTable);

        }
    }
}