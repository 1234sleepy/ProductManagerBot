using System.ComponentModel.DataAnnotations;

namespace ProductManagerBot.Data.Entities;

public class Product : AddedDateEntity, IEntity
{
    public int Id { get; set; }
    public string? Barcode { get; set; }
    public double Weight { get; set; }
    public double? Calories { get; set; }
    public DateTime DateOfManufacture { get; set; }
    public DateTime DateOfUse { get; set; }
    public string? ProductContent { get; set; }
    public int? ManufactureId { get; set; }
    [Required]
    [MaxLength(25)]
    public string? Name { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }


    public Manufacture? Manufacture { get; set; }
    public Category? Category { get; set; }
}
