namespace WebApiCore.Models;

public class City : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Link { get; set; }
}