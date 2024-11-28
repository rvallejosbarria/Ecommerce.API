namespace Ecommerce;

public static class ApiEndpoints
{
    private const string Base = "api/v1";

    public static class Categories
    {
        private const string Base = $"{ApiEndpoints.Base}/categories";

        public const string Create = Base;
        public const string Get = $"{Base}/{{idOrSlug}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}
