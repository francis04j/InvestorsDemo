using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.DataMappers;
using Preqin.InvestorsApi.DTOs;

namespace Preqin.InvestorsApi.DataService;

public class InvestorDataService(IInvestorRepository investorRepository, IInvestorMapper investorMapper) : IInvestorDataService
{
    public IEnumerable<InvestorSummaryDto> GetSummaryForAllInvestors()
    {
        //call repo
        var investorsEntities = investorRepository.GetInvestors();
        //map repo response
        var mapDta = investorMapper.Map(investorsEntities);
        //return result
        return mapDta;
    }
}