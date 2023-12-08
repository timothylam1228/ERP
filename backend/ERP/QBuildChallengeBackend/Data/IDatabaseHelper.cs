using ERP.Models;

namespace ERP.Data;

public interface IDatabaseHelper
{
    IEnumerable<PartModel> GetParts();
    IEnumerable<BOMModel> GetBOMs();
}