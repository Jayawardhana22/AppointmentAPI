namespace AppointmentAPI.Models;

public class Appointment
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public Shop Shop { get; set; } = null!;
    public int CustomerId { get; set; }
    public User Customer { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public string Status { get; set; } = "Pending"; // "Pending", "Confirmed", "Cancelled"
    public string Notes { get; set; } = string.Empty;
}