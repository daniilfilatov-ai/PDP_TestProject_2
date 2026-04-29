using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

public sealed class InputEntryDetails
{
    [JsonPropertyName("sku_code")]
    public string SkuCode { get; set; } = string.Empty;

    [JsonPropertyName("display_title")]
    public string DisplayTitle { get; set; } = string.Empty;

    [JsonPropertyName("unit_cost_cents")]
    public int UnitCostCents { get; set; }

    [JsonPropertyName("group_info")]
    public InputGroupInfo GroupInfo { get; set; } = null!;
}
