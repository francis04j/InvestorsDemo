namespace Preqin.InvestorsApi.DTOs;

public class CommitmentDto
{
    public required string AssetClass { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
}