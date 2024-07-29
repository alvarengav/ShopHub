namespace OrderService.Application.DTOs;

public record CreateOrderRequest(string UserId, IReadOnlyList<OrderItemDTO> OrderItems);
