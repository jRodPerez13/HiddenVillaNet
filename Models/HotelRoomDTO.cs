using System.ComponentModel.DataAnnotations;

namespace Models;

public class HotelRoomDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter room name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter Occupancy")]
    public int Occupancy { get; set; }
    [Range(1, 3000, ErrorMessage = "Regular rate must be between 1 and 3000")]
    public double RegularRate { get; set; }
    public string? Details { get; set; }
    public string? SqFt { get; set; }
    public double TotalDays { get; set; }
    public double TotalAmount { get; set; }
    public virtual ICollection<HotelRoomImageDTO>? HotelRoomImages { get; set; }
    public List<string>? ImageUrls { get; set; }
    public bool IsBooked { get; set; }
}

