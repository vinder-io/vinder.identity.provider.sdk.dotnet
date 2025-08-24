namespace Vinder.IdentityProvider.Sdk.Serializers;

public static class JsonSerialization
{
    public static JsonSerializerOptions SerializerOptions => new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}