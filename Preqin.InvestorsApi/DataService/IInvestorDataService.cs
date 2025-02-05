using Preqin.InvestorsApi.DTOs;

namespace Preqin.InvestorsApi.DataService;

public interface IInvestorDataService
{
    IEnumerable<InvestorSummaryDto> GetSummaryForAllInvestors();
}