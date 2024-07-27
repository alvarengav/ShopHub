using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries;

public record GetAllOrdersWithItemsQuery : IRequest<IEnumerable<Order>>;
