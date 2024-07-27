using MediatR;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands;

public record AddOrderItemCommand(
    OrderId OrderId,
    ProductId ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity
) : IRequest<Unit>;
