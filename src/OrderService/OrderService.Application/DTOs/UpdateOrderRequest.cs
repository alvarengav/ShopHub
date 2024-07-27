namespace OrderService.Application.DTOs;

public record UpdateOrderRequest(long UserId, IReadOnlyList<OrderItemDTO> OrderItems);
