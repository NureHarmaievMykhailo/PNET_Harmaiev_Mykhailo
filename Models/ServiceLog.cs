using System.ComponentModel.DataAnnotations;

namespace AutoWorkshopWeb.Models;

public class ServiceLog
{
    [Key]
    public int LogId { get; set; }

    public int? ServiceId { get; set; }

    [StringLength(20, ErrorMessage = "Тип операції не може перевищувати 20 символів")]
    public string? OperationType { get; set; }

    [StringLength(255, ErrorMessage = "Повідомлення не може перевищувати 255 символів")]
    public string? Message { get; set; }

    public DateTime LogDate { get; set; } = DateTime.Now;

    public Service? Service { get; set; }
}
