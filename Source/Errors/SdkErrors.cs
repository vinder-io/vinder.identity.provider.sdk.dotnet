namespace Vinder.Federation.Sdk.Errors;

public static class SdkErrors
{
    public static readonly Error DeserializationFailure = new(
        Code: "#VINDER-SDK-ERR-001",
        Description: "Failed to deserialize the response payload. The response format may be invalid or unexpected."
    );

    public static readonly Error HttpRequestFailure = new(
        Code: "#VINDER-SDK-ERR-002",
        Description: "The HTTP request failed due to a network error, timeout, or unexpected response."
    );

    public static readonly Error Unauthorized = new(
        Code: "#VINDER-SDK-ERR-003",
        Description: "The request was not authorized. Please check your credentials or tenant configuration."
    );

    public static readonly Error Unknown = new(
        Code: "#VINDER-SDK-ERR-999",
        Description: "An unknown error occurred in the SDK."
    );
}