using System.Text.Json.Serialization;
using AspNetCore.Hashids.Json;
using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.WebApi.DTOs;

public class WorkBookDTO
{
    [JsonConverter(typeof(HashidsJsonConverter))]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}