using MediatR;
using OrderService.Application.Queries;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;

namespace OrderService.Application.Handlers;

public class GetAllOrdersQueryHanler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHanler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _orderRepository.GetAllOrderAsync();
    }
}
