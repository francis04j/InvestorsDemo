using System.Collections.Generic;
using System.Threading.Tasks;
using Preqin.InvestorsApi.DTOs;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.Data
{
    public interface IInvestorRepository
    {
        IQueryable<Investor> GetInvestors();
        Task<IEnumerable<CommitmentDto>> GetCommitmentsAsync(int investorId, string? assetClass);
    }
}