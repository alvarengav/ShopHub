namespace OrderService.Application.DTOs;

public record OrderItemDTO(long ProductId, string ProductName, decimal UnitPrice, int Quantity);
