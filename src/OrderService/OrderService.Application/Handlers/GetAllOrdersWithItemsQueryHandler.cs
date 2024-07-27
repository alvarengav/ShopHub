using MediatR;
using OrderService.Application.Queries;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Handlers;

public class GetAllOrdersWithItemsQueryHandler
    : IRequestHandler<GetAllOrdersWithItemsQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersWithItemsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> Handle(
        GetAllOrdersWithItemsQuery request,
        CancellationToken cancellationToken
    )
    {
        var orders = await _orderRepository.GetAllOrdersWithItemsAsync();
        return orders;
    }
}
