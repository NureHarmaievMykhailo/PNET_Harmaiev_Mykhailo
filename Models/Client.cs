using System.ComponentModel.DataAnnotations;

namespace AutoWorkshopWeb.Models;

public class Client
{
    public int ClientId { get; set; }

    [Required(ErrorMessage = "ПІБ обов’язкове")]
    [StringLength(100, ErrorMessage = "Максимум 100 символів")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон обов’язковий")]
    [Phone(ErrorMessage = "Невірний формат телефону")]
    public string? Phone { get; set; }

    [EmailAddress(ErrorMessage = "Невірний формат email")]
    public string? Email { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}