namespace Products.Core;

public class Product
{
    public Guid Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public DateTime ScannedAt { get; set; } = DateTime.UtcNow;
}
