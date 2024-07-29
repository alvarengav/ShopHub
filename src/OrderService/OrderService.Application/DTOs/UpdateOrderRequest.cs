namespace OrderService.Application.DTOs;

public record UpdateOrderRequest(string UserId, IReadOnlyList<OrderItemDTO> OrderItems);
