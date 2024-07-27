using MediatR;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands;

public record RemoveOrderItemCommand(OrderId OrderId, OrderItemId OrderItemId) : IRequest<Unit>;
