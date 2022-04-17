using System.Text.Json.Serialization;
using AspNetCore.Hashids.Json;

namespace OnPaper.ExpenseTracker.WebApi.DTOs;

public class TransactionDTO
{
    [JsonConverter(typeof(HashidsJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public int TransactionTypeId { get; set; }
    public int CategoryId { get; set; }
    public int PaymentTypeId { get; set; }
    [JsonConverter(typeof(HashidsJsonConverter))]
    public int WorkBookId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}