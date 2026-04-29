using System.Text.Json.Serialization;

namespace PDP_TestProject_2.Infrastructure.Integrations.FakeCompData.Models;

public sealed class InputOrderModel
{
    [JsonPropertyName("transaction_id")]
    public int TransactionId { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime TimeStamp { get; set; }

    [JsonPropertyName("operator_name")]
    public string OperatorName { get; set; } = string.Empty;

    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    [JsonPropertyName("current_state")]
    public string CurrentState { get; set; } = string.Empty;

    [JsonPropertyName("total_amount_cents")]
    public int TotalAmountCents { get; set; }

    [JsonPropertyName("line_items")]
    public List<InputLineItem> LineItems { get; set; } = [];
}
