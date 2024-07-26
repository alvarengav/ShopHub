using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetOrders()
    {
        return Ok(new { Meessage = "Orders endpoint" });
    }
}
