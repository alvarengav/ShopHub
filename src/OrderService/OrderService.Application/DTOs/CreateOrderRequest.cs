namespace OrderService.Application.DTOs;

public record CreateOrderRequest(long UserId, IReadOnlyList<OrderItemDTO> OrderItems);
