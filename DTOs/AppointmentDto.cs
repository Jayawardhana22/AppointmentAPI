namespace AppointmentAPI.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public string ShopName { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}