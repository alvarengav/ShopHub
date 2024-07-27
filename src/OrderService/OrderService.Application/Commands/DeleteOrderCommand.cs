using MediatR;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Commands;

public record DeleteOrderCommand(OrderId OrderId) : IRequest<Unit>;
