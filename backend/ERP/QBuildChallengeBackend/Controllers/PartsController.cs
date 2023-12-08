using ERP.Data;
using Microsoft.AspNetCore.Mvc;

namespace ERP.QBuildChallengeBackend.Controllers;

public class PartsController : ControllerBase
{
    private readonly IDatabaseHelper _databaseHelper;

    public PartsController(IDatabaseHelper databaseHelper)
    {
        _databaseHelper = databaseHelper;
    }

    [HttpGet]
    public IActionResult GetParts()
    {
        var parts = _databaseHelper.GetParts();
        return Ok(parts);
    }   
}