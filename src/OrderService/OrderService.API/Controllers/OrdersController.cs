using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.DTOs;
using OrderService.Application.Queries;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] bool includeItems = false)
    {
        var orders = Enumerable.Empty<Order>();
        if (includeItems)
        {
            orders = await _mediator.Send(new GetAllOrdersWithItemsQuery());
            return Ok(orders);
        }

        orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(long id, [FromQuery] bool includeItems = false)
    {
        var query = new GetOrderByIdQuery(id, includeItems);
        var order = await _mediator.Send(query);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddOrderItem(long id, [FromBody] OrderItemDTO request)
    {
        var command = new AddOrderItemCommand(
            new OrderId(id),
            new ProductId(request.ProductId),
            request.ProductName,
            request.UnitPrice,
            request.Quantity
        );
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/items/{orderItemId}")]
    public async Task<IActionResult> UpdateOrderItem(
        long id,
        long orderItemId,
        [FromBody] OrderItemDTO request
    )
    {
        var command = new UpdateOrderItemCommand(
            id,
            orderItemId,
            request.ProductId,
            request.ProductName,
            request.UnitPrice,
            request.Quantity
        );
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}/items/{orderItemId}")]
    public async Task<IActionResult> RemoveOrderItem(long id, long orderItemId)
    {
        var command = new RemoveOrderItemCommand(new OrderId(id), new OrderItemId(orderItemId));
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(request.UserId, request.OrderItems);
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(long id, [FromBody] UpdateOrderRequest request)
    {
        var command = new UpdateOrderCommand(id, request.UserId, request.OrderItems);

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id)
    {
        await _mediator.Send(new DeleteOrderCommand(new OrderId(id)));
        return NoContent();
    }
}
