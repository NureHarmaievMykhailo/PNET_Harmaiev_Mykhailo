using System.ComponentModel.DataAnnotations;

namespace AutoWorkshopWeb.Models;

public class Order
{
    public int OrderId { get; set; }

    [Required(ErrorMessage = "Авто обов’язкове")]
    public int CarId { get; set; }

    [Required(ErrorMessage = "Послуга обов’язкова")]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "Дата обов’язкова")]
    public DateTime OrderDate { get; set; }

    [Required(ErrorMessage = "Кількість обов’язкова")]
    [Range(1, 100, ErrorMessage = "Кількість має бути від 1 до 100")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Статус обов’язковий")]
    [StringLength(20, ErrorMessage = "Статус не може перевищувати 20 символів")]
    public string? Status { get; set; }

    public Car? Car { get; set; }
    public Service? Service { get; set; }
}
