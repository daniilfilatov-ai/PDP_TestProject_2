using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Application.InputDataStyles.FakeCompDataStyle;

public sealed class InputGroupInfo
{
    [JsonPropertyName("group_id")]
    public int GroupId { get; set; }
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;
    [JsonPropertyName("details")]
    public string? Details { get; set; }
}
