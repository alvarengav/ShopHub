using MediatR;
using OrderService.Domain.Entities;

namespace OrderService.Application.Queries;

public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>;
