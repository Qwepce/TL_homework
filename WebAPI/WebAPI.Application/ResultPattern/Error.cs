using System.Text.Json.Serialization;

namespace WebAPI.Application.ResultPattern;

public class Error
{
    [JsonPropertyName( "Error message" )]
    public string ErrorDescription { get; init; }

    public Error( string errorDescription )
    {
        ErrorDescription = errorDescription;
    }
}
