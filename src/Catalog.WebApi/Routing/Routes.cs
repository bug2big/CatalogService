namespace Catalog.WebApi.Routing;

public static class Routes
{
    internal static class CategoryController
    {
        internal const string Endpoint = "categories";

        internal static class Action
        {
            internal const string Delete = "{categoryId:required}";
            internal const string GetAll = "";
            internal const string Create = "";
            internal const string Update = "";
        }
    }

    internal static class ProductController
    {
        internal const string Endpoint = "products";

        internal static class Action
        {
            internal const string Delete = "{productId:required}";
            internal const string GetAll = "";
            internal const string Create = "";
            internal const string Update = "";
        }
    }
}