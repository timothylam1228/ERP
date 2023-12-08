using ERP.Models;
using System.Data.SqlClient;

namespace ERP.Data;

public class DatabaseHelper : IDatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = "Server=localhost;Database=master;User Id=sa;Password=TooSimple123@@;";
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
                            Name = reader["NAME"].ToString(),
                            Title = reader["TITLE"].ToString(),
                            Type = reader["TYPE"].ToString(),
                            Item = reader["ITEM"].ToString(),
                            Material = reader["MATERIAL"].ToString(),
                            PartNumber = reader["PART_NUMBER"].ToString()
                        };
                        Console.WriteLine(part);

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
                         
                            ComponentName = reader["COMPONENT_NAME"].ToString(),
                            ParentName = reader["PARENT_NAME"].ToString(),
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