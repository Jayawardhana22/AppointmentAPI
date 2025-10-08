namespace AppointmentAPI.Models;

public class Shop
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string OpeningHours { get; set; } = string.Empty; // Could be JSON string
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}