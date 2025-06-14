using System.ComponentModel.DataAnnotations;

namespace AutoWorkshopWeb.Models;

public class Service
{
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "Назва послуги обов’язкова")]
    [StringLength(100, ErrorMessage = "Максимум 100 символів")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 10000, ErrorMessage = "Ціна має бути від 0 до 10000")]
    public decimal Price { get; set; }

    [Range(1, 1440, ErrorMessage = "Тривалість має бути від 1 до 1440 хвилин")]
    public int? EstimatedDuration { get; set; }

    [StringLength(500, ErrorMessage = "Опис не може перевищувати 500 символів")]
    public string? Description { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<ServiceLog> Logs { get; set; } = new List<ServiceLog>();
}
