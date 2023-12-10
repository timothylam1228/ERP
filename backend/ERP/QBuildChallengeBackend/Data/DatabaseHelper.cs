using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Data;

public class DatabaseHelper : IDatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IEnumerable<PartModel> GetParts()
    {
        var parts = new List<PartModel>();
        using (var connection = new SqlConnection(_connectionString))
        {
            
            connection.Open();
            var command = new SqlCommand("SELECT * From dbo.part", connection);
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var part = new PartModel
                        {
                            Name = reader["NAME"]?.ToString() ?? string.Empty,
                            Title = reader["TITLE"]?.ToString() ?? string.Empty,
                            Type = reader["TYPE"]?.ToString() ?? string.Empty,
                            Item = reader["ITEM"]?.ToString() ?? string.Empty,
                            Material = reader["MATERIAL"]?.ToString() ?? string.Empty,
                            PartNumber = reader["PART_NUMBER"]?.ToString() ?? string.Empty
                        };
                        parts.Add(part);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
            }
        }
        return parts;
    }
    
    public IEnumerable<BOMModel> GetBOMs()
    {
        var boms = new List<BOMModel>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * From dbo.BOM", connection);

            try
            {
                using (var reader = command.ExecuteReader())
                {
                
                    while (reader.Read())
                    {
                        var bom = new BOMModel
                        {
                            ComponentName = reader["COMPONENT_NAME"]?.ToString() ?? string.Empty,
                            ParentName = reader["PARENT_NAME"]?.ToString() ?? string.Empty,
                            Quantity = reader["QUANTITY"] == DBNull.Value ? 0 : Convert.ToInt32(reader["QUANTITY"])
                          //Handle nulls  
                        };

                        boms.Add(bom);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
            }
        }
        return boms;
    }
}