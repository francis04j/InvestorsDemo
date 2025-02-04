using Preqin.InvestorsApi.DTOs;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.DataMappers;

public interface IInvestorMapper
{
    InvestorSummaryDto Map(Investor investor);
}