using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Application.InputDataStyles.FakeCompDataStyle;

public sealed class InputLineItem
{
    [JsonPropertyName("entry_details")]
    public InputEntryDetails EntryDetails { get; set; } = null!;
    [JsonPropertyName("unit_count")]
    public int UnitCount { get; set; }
}
