namespace ERP.Models;

public class BOMModel
{
    public string COMPONENT_NAME { get; set; }
    public string PART_NUMBER { get; set; }

    public string QUANTITY { get; set; }
    // Add other properties as needed based on the actual BOM CSV file structure
}