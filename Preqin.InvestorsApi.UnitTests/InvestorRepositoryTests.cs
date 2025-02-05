using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.Models;
using Bogus;
using Moq;
using Microsoft.Extensions.Logging;
using Moq.EntityFrameworkCore;

namespace Preqin.InvestorsApi.UnitTests
{
    public class InvestorRepositoryTests
    {
        private readonly Mock<IAppDbContext> _context;
        private readonly InvestorRepository _repository;
        private readonly Mock<ILogger<InvestorRepository>> _loggerMock;

        public InvestorRepositoryTests()
        {
            _context = new Mock<IAppDbContext>();
            _loggerMock = new Mock<ILogger<InvestorRepository>>();
            _repository = new InvestorRepository(_context.Object, _loggerMock.Object);
        }

        // [Fact]
        // public async Task GetInvestorsAsync_ReturnsInvestors_WhenInvestorsExist()
        // {
        //     // Given
        //     var investorId = 1;
        //     var faker = new Faker<Investor>()
        //         .RuleFor(i => i.InvestorId, f => investorId++)
        //         .RuleFor(i => i.Name, f => f.Company.CompanyName())
        //         .RuleFor(i => i.Type, f => f.Company.CompanySuffix())
        //         .RuleFor(i => i.Country, f => f.Address.Country())
        //         .RuleFor(i => i.Commitments, f => new List<Commitment>().AsQueryable());
        //
        //     var investors = faker.Generate(2);
        //
        //     _context.Setup(x => x.Investors).ReturnsDbSet(investors);
        //
        //     // When
        //     var result = await _repository.GetInvestorsAsync();
        //
        //     // Then
        //     Assert.Equal(2, result.Count());
        // }

        // [Fact]
        // public async Task GetInvestorsAsync_ReturnsEmptyList_WhenNoInvestorsExist()
        // {
        //     // Given
        //     _context.Setup(x => x.Investors).ReturnsDbSet(new List<Investor>());
        //     
        //     //when
        //     var result = await _repository.GetInvestorsAsync();
        //
        //     // Then
        //     Assert.Empty(result);
        //     // _loggerMock.Verify(
        //     //     x => x.Log(
        //     //         LogLevel.Warning,
        //     //         It.IsAny<EventId>(),
        //     //         It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No investors found.")),
        //     //         It.IsAny<Exception>(),
        //     //         It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
        //     //     Times.Once);
        // }
        
       // [Fact]
        // public async Task GetInvestorsAsync_ThrowsAndLogException_WhenDatabaseFails()
        // {
        //     // Given
        //     _context.Setup(x => x.Investors).Throws<Exception>();
        //
        //     // When + then
        //     await Assert.ThrowsAsync<Exception>(() => _repository.GetInvestorsAsync());
        //     _loggerMock.Verify(
        //         x => x.Log(
        //             LogLevel.Error,
        //             It.IsAny<EventId>(),
        //             It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred while retrieving investors.")),
        //             It.IsAny<Exception>(),
        //             It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
        //         Times.Once);
        // }
        
        // [Fact]
        // public async Task GetInvestorsAsync_PopulatesInvestorSummaryDtoFieldsCorrectly()
        // {
        //     // Given
        //     var commitmentId = 1;
        //     var commitmentFaker = new Faker<Commitment>()
        //         .RuleFor(c => c.CommitmentId, f => commitmentId++)
        //         .RuleFor(c => c.Amount, f => f.Finance.Amount())
        //         .RuleFor(c => c.AssetClass, f => f.Commerce.ProductName());
        //
        //     var commitments = commitmentFaker.Generate(3);
        //     commitments[0].Amount = 100;
        //     commitments[1].Amount = 200;
        //     commitments[2].Amount = 300;
        //
        //     var investorId = 1;
        //     var investorFaker = new Faker<Investor>()
        //         .RuleFor(i => i.InvestorId, f => investorId++)
        //         .RuleFor(i => i.Name, f => f.Company.CompanyName())
        //         .RuleFor(i => i.Type, f => f.Company.CompanySuffix())
        //         .RuleFor(i => i.Country, f => f.Address.Country())
        //         .RuleFor(i => i.Commitments, f => commitments.AsQueryable());
        //     
        //     var investor = investorFaker.Generate(1);
        //    _context.Setup(x => x.Investors).ReturnsDbSet(investor);
        //
        //     // When
        //     var result = await _repository.GetInvestorsAsync();
        //     var investorSummary = result.First();
        //
        //     // Then
        //     Assert.NotNull(investorSummary);
        //     Assert.Equal(investor.First().InvestorId, investorSummary.InvestorId);
        //     Assert.Equal(investor.First().Name, investorSummary.Name);
        //     Assert.Equal(investor.First().Type, investorSummary.Type);
        //     Assert.Equal(investor.First().Country, investorSummary.Country);
        //     Assert.Equal(600, investorSummary.TotalCommitments);
        // }
        
        [Fact]
        public async Task GetCommitmentsAsync_ReturnsCommitments_WhenCommitmentsExist()
        {
            // Given
            
        
            var investorId = 1;
            var investorFaker = new Faker<Investor>()
                .RuleFor(i => i.InvestorId, f => investorId)
                .RuleFor(i => i.Name, f => f.Company.CompanyName())
                .RuleFor(i => i.Type, f => f.Company.CompanySuffix())
                .RuleFor(i => i.Country, f => f.Address.Country());
            
            var commitmentId = 1;
            var commitmentFaker = new Faker<Commitment>()
                .RuleFor(c => c.CommitmentId, f => commitmentId++)
                .RuleFor(c => c.Amount, f => f.Finance.Amount())
                .RuleFor(c => c.AssetClass, f => f.Commerce.ProductName())
                .RuleFor(c => c.Currency, f => f.Finance.Currency().Code)
                .RuleFor(c => c.InvestorId, f => investorId);
            var commitments = commitmentFaker.Generate(3);

            investorFaker.RuleFor(i => i.Commitments, f => commitments).Generate();
            
            
            _context.Setup(x => x.Commitments).ReturnsDbSet(commitments);
        
            // Act
            var result = await _repository.GetCommitmentsAsync(investorId, null);
        
            // Assert
            Assert.Equal(3, result.Count());
        }
        
        [Fact]
        public async Task GetCommitmentsAsync_ReturnsEmptyList_WhenNoCommitmentsExist()
        {
            //Given
            _context.Setup(x => x.Commitments).ReturnsDbSet(new List<Commitment>());
            
            // Act
            var result = await _repository.GetCommitmentsAsync(1, null);
        
            // Assert
            Assert.Empty(result);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No commitments found for investor with ID 1.")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
        
        [Fact]
        public async Task GetCommitmentsAsync_FiltersByAssetClass()
        {
            // Given
            var commitmentId = 1;
            var commitmentFaker = new Faker<Commitment>()
                .RuleFor(c => c.CommitmentId, f => commitmentId++)
                .RuleFor(c => c.Amount, f => f.Finance.Amount())
                .RuleFor(c => c.AssetClass, f => f.Commerce.ProductName())
                .RuleFor(c => c.Currency, f => f.Finance.Currency().Code);
        
            var commitments = commitmentFaker.Generate(3);
            commitments[0].AssetClass = "Infrastructure";
            commitments[1].AssetClass = "Real Estate";
            commitments[2].AssetClass = "Infrastructure";
        
            var investorId = 1;
            var investorFaker = new Faker<Investor>()
                .RuleFor(i => i.InvestorId, f => investorId)
                .RuleFor(i => i.Name, f => f.Company.CompanyName())
                .RuleFor(i => i.Type, f => f.Company.CompanySuffix())
                .RuleFor(i => i.Country, f => f.Address.Country())
                .RuleFor(i => i.Commitments, f => commitments);
        
            var investor = investorFaker.Generate();
            _context.Setup(x => x.Commitments).ReturnsDbSet(commitments);
        
            // when
            var result = await _repository.GetCommitmentsAsync
                (investorId, "Infrastructure");
        
            // then
            Assert.All(result, c => Assert.Equal("Infrastructure", c.AssetClass));
        }
        
        [Fact]
        public async Task GetCommitmentsAsync_ThrowsAndLogsException_WhenDatabaseFails()
        {
            // Given
            _context.Setup(x => x.Commitments).Throws<Exception>();
        
            // When + then
            await Assert.ThrowsAsync<Exception>(() => _repository.GetCommitmentsAsync(1, null));

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred while retrieving commitments for investor with ID")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }

    }
}