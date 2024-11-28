namespace Ecommerce.Models;

using System.Text.RegularExpressions;

public partial class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug => GenerateSlug();
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();

    private string GenerateSlug()
    {
        var slugTitle = SlugRegex().Replace(Name, string.Empty)
            .ToLower().Replace(" ", "-");
        return slugTitle;
    }

    [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
    private static partial Regex SlugRegex();
}
