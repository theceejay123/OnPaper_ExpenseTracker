using System.Text.Json.Serialization;
using AspNetCore.Hashids.Json;

namespace OnPaper.ExpenseTracker.WebApi.DTOs;

public class TransactionResponseDTO
{
    [JsonConverter(typeof(HashidsJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public string TransactionType { get; set; }
    public string Category { get; set; }
    public string PaymentType { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}