namespace Preqin.InvestorsApi.DTOs;

public class InvestorSummaryDto
{
    public int InvestorId { get; set; }
    public required string Name { get; set; }
    public decimal TotalCommitments { get; set; }
    public required string Type { get; set; }
    
    public required string Country { get; set; }
}