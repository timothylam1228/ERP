namespace ERP.Models;

public class BOMModel
{
    public string ComponentName { get; set; }
    public string ParentName { get; set; }
    public int Quantity { get; set; }
    // Add other properties as needed based on the actual BOM CSV file structure
}