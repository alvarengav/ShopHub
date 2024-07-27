using MediatR;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands;

public record UpdateOrderItemCommand(
    OrderId OrderId,
    OrderItemId OrderItemId,
    ProductId ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity
) : IRequest<Unit>;
