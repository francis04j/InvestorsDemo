using Microsoft.EntityFrameworkCore;
using Preqin.InvestorsApi.DTOs;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.Data
{
    public class InvestorRepository : IInvestorRepository
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<InvestorRepository> _logger;

        public InvestorRepository(IAppDbContext context, ILogger<InvestorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IQueryable<Investor> GetInvestors()
        {
            try
            {
                var investors =  _context.Investors;
                return investors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving investors.");
                throw;
            }
        }

        public async Task<IEnumerable<CommitmentDto>> GetCommitmentsAsync(int investorId, string? assetClass)
        {
            try
            {
                var query = _context.Commitments as IQueryable<Commitment>;

                if (!string.IsNullOrEmpty(assetClass))
                {
                    query = query.Where(c => c.AssetClass == assetClass);
                }

                var commitments = await query
                    .Where(c => c.InvestorId == investorId).ToListAsync();
                    
                var commitmentsDtos = commitments.Select(c => new CommitmentDto
                    {
                        Amount = c.Amount,
                        AssetClass = c.AssetClass,
                        Currency = c.Currency,
                    })
                    .ToList();

                if (commitmentsDtos.Count == 0)
                {
                    _logger.LogWarning("No commitments found for investor with ID {InvestorId}.", investorId);
                    return [];
                }

                return commitmentsDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving commitments for investor with ID {InvestorId}.", investorId);
                throw;
            }
        }
    }
}