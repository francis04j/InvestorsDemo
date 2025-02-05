using Preqin.InvestorsApi.DTOs;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.DataMappers;

public class InvestorMapper : IInvestorMapper
{
    public InvestorSummaryDto Map(Investor investor)
    {
        ArgumentNullException.ThrowIfNull(investor);

        return new InvestorSummaryDto()
        {
            Country = investor.Country ?? "Unknown",
            Name = investor.Name ?? "Unknown",
            DateAdded = investor.DateAdded,
            InvestorId = investor.InvestorId,
            TotalCommitments = GetTotalCommitments(investor.Commitments),
            Type = investor.Type
        };
    }

    public IEnumerable<InvestorSummaryDto> Map(IQueryable<Investor> investor)
    {
        ArgumentNullException.ThrowIfNull(investor, nameof(investor));
        
        return investor.AsEnumerable().Select(Map);
    }

    //for data repo backed by IQueryable, it is better to offload the aggregation to SQL server
    private decimal GetTotalCommitments(ICollection<Commitment>? commitments)
    {
        if (commitments == null) return 0;
        
        var totalCommitments = commitments.Any() ? 
            commitments.Sum(x => (decimal?)x.Amount) ?? 0 //proper SQL translation of null to 0
                : 0;
        
        return totalCommitments;
    }
}