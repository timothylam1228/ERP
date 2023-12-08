using ERP.Data;
using Microsoft.AspNetCore.Mvc;

namespace ERP.QBuildChallengeBackend.Controllers;

public class BomController : ControllerBase
{
    private readonly IDatabaseHelper _databaseHelper;
    
    public BomController(IDatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }
    
    [HttpGet]
    public IActionResult GetBOMs()
    {
        var boms = _databaseHelper.GetBOMs();
        return Ok(boms);
    }
}