using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands;

public readonly record struct UpdateOrderCommand(
    long OrderId,
    long UserId,
    IReadOnlyList<OrderItemDTO> OrderItems
) : IRequest<Unit>;
