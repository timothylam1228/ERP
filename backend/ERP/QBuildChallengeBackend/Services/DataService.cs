using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ERP.Models;

public class DataService
{
    private readonly string bomFilePath;
    private readonly string partFilePath;

    public DataService(string bomFilePath, string partFilePath)
    {
        this.bomFilePath = bomFilePath;
        this.partFilePath = partFilePath;
    }

    public List<BOMModel> GetBOMData()
    {
        using (var reader = new StreamReader(bomFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<BOMModel>().ToList();
        }
    }

    public List<PartModel> GetPartData()
    {
        using (var reader = new StreamReader(partFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<PartModel>().ToList();
        }
    }
}