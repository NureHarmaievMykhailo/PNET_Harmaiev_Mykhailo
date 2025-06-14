using System.ComponentModel.DataAnnotations;

namespace AutoWorkshopWeb.Models;

public class Car
{
    public int CarId { get; set; }

    [Required(ErrorMessage = "Клієнт обов’язковий")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "Номер авто обов’язковий")]
    [StringLength(10, ErrorMessage = "Номер авто не може перевищувати 10 символів")]
    public string LicensePlate { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Марка не може перевищувати 50 символів")]
    public string? Brand { get; set; }

    [StringLength(50, ErrorMessage = "Модель не може перевищувати 50 символів")]
    public string? Model { get; set; }

    [Range(1900, 2025, ErrorMessage = "Рік має бути від 1900 до 2025")]
    public int? Year { get; set; }

    public Client? Client { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
