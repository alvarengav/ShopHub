using MediatR;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Application.Queries;

public record GetOrderByIdQuery(OrderId OrderId, bool IncludeItems = false) : IRequest<Order?>;
