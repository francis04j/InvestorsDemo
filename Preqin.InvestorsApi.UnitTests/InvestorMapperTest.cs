using Bogus;
using FluentAssertions;
using Preqin.InvestorsApi.DataMappers;
using Preqin.InvestorsApi.DTOs;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.UnitTests;

public class InvestorMapperTest
{
    private readonly InvestorMapper _sut;
    readonly Faker<Investor> _fakerInvestor = new();
    
    public InvestorMapperTest()
    {
        _sut = new InvestorMapper();
    }

    [Fact]
    public void InvestorMapper_ShouldMap_WhenInvestorEntityIsValid()
    {
        //given
        int investorId = 1;
        var commitments = new Faker<Commitment>().
            RuleFor(x => x.Amount, f => f.Random.Decimal(10))
            .Generate(1);
        Investor investorEntity = _fakerInvestor
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.InvestorId, _ => investorId++)
            .RuleFor(x => x.DateAdded, f => f.Date.Past())
            .RuleFor(x => x.Commitments, commitments)
            .RuleFor(x => x.Country, f => f.Address.Country())
            .RuleFor(x => x.Type, f => f.Company.CompanyName()).
            Generate();
                
        
        //when
        InvestorSummaryDto result = _sut.Map(investorEntity);
        
        //then
        result.InvestorId.Should().Be(investorEntity.InvestorId);
        result.Name.Should().Be(investorEntity.Name);
        result.DateAdded.Should().Be(investorEntity.DateAdded);
        result.TotalCommitments.Should().Be(investorEntity.Commitments.Sum(x=>x.Amount));
        result.Type.Should().Be(investorEntity.Type);
    }
    
    [Fact]
    public void InvestorMapper_ShouldMap_WhenInvestorEntityIsInValid()
    {
        //given
        Investor investorEntity = null;
        
        //when
        Action action = () => _sut.Map(investorEntity);
        
        //then
        Assert.Throws<ArgumentNullException>(action);
    }
}