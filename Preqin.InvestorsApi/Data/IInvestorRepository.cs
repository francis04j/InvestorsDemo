using System.Collections.Generic;
using System.Threading.Tasks;
using Preqin.InvestorsApi.DTOs;

namespace Preqin.InvestorsApi.Data
{
    public interface IInvestorRepository
    {
        Task<IEnumerable<InvestorSummaryDto>> GetInvestorsAsync();
        Task<IEnumerable<CommitmentDto>> GetCommitmentsAsync(int investorId, string? assetClass);
    }
}