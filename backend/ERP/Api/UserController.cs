using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        // Your code to get user data
        return Ok("API is working!");
    }

    // Other actions (POST, PUT, DELETE, etc.) can be added here
}