using Bogus;
using FluentAssertions;
using Moq;
using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.DataMappers;
using Preqin.InvestorsApi.DataService;
using Preqin.InvestorsApi.DTOs;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.UnitTests;

public class InvestorDataServiceTests
{
    readonly InvestorDataService _sut;
    readonly Mock<IInvestorRepository> _repositoryMock;
    readonly Mock<IInvestorMapper> _investorMapperMock;

    public InvestorDataServiceTests()
    {
        _repositoryMock = new Mock<IInvestorRepository>();
        _investorMapperMock = new Mock<IInvestorMapper>();
        _sut = new InvestorDataService(_repositoryMock.Object, _investorMapperMock.Object);
    }

    [Fact]
    public void GetSummaryForAllInvestors_ShouldReturnAllInvestorsSummary()
    {
        //given
        var investors = new Faker<Investor>().Generate(1);
        var investorsSummary = new Faker<InvestorSummaryDto>().Generate(1);
        _repositoryMock.Setup(x => x.GetInvestors()).Returns(investors.AsQueryable());
        _investorMapperMock.Setup(x => x.Map(It.IsAny<IQueryable<Investor>>()))
            .Returns(investorsSummary);
       
        //when
        var result = _sut.GetSummaryForAllInvestors();
        
        //then
        result.Count().Should().Be(1);
        result.Should().BeEquivalentTo(investorsSummary);
    }
}