using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands;

public readonly record struct CreateOrderCommand(
    string userId,
    IReadOnlyList<OrderItemDTO> orderItems
) : IRequest<OrderId>;
