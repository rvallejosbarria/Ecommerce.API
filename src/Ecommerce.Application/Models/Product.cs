namespace Ecommerce.Models;

using System.Text.RegularExpressions;

public partial class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug => GenerateSlug();
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Image { get; set; }
    // public Guid CategoryId { get; set; }
    // public Category Category { get; set; } = null!;

    private string GenerateSlug()
    {
        var slugTitle = SlugRegex().Replace(Name, string.Empty)
            .ToLower().Replace(" ", "-");
        return slugTitle;
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}
