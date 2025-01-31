using System.ComponentModel.DataAnnotations.Schema;

namespace Preqin.InvestorsApi.Models;

public class Commitment
{
    [Column("commitment_id")]
    public int CommitmentId { get; set; }
    
    [Column("investor_id")]
    public int InvestorId { get; set; }
    
    [Column("asset_class")]
    public required string AssetClass { get; set; }
    
    [Column("amount")]
    public decimal Amount { get; set; }
    
    [Column("currency")]
    public required string Currency { get; set; }   
    public required Investor Investor { get; set; }
}