using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Preqin.InvestorsApi.Controllers;
using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.DTOs;

namespace Preqin.InvestorsApi.UnitTests;

public class InvestorsControllerTests
{
    private readonly Mock<IInvestorRepository> _repo;
    private readonly Mock<ILogger<InvestorsController>> _logger;
    private readonly InvestorsController _sut;

    public InvestorsControllerTests()
    {
        _repo = new Mock<IInvestorRepository>();
        _logger = new Mock<ILogger<InvestorsController>>();
        _sut = new InvestorsController(_repo.Object, _logger.Object);
    }

    [Fact]
    public async Task GetInvestors_ReturnsOkResult_WithListOfInvestors()
    {
        // Given
        var investorsSummary = new Faker<InvestorSummaryDto>().Generate();
        _repo.Setup(x => x.GetInvestorsAsync())
            .ReturnsAsync(new List<InvestorSummaryDto> { investorsSummary });

        // When
        var result = await _sut.GetInvestors();

        // Then
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<InvestorSummaryDto>>(okResult.Value);

        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetInvestors_Logs_RequestCalls()
    {
        // Given
        var investorsSummary = new Faker<InvestorSummaryDto>().Generate();
        _repo.Setup(x => x.GetInvestorsAsync())
            .ReturnsAsync(new List<InvestorSummaryDto> { investorsSummary });
        
        // When
        await _sut.GetInvestors();

        // Then
       // Verify logger calls
           _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Getting investors list")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);


        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Returning investors list")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);

    }

    [Fact]
    public async Task GetCommitments_ReturnsOkResult_WithListOfCommitments()
    {
        // Given
        var commitments = new Faker<CommitmentDto>().Generate(1);

        _repo.Setup(x => x.GetCommitmentsAsync(1, null))
            .ReturnsAsync(commitments);

        // When
        var result = await _sut.GetCommitments(1, null);

        // Then
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CommitmentDto>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetCommitments_ReturnsNotFound_WhenNoCommitments()
    {
        // Given    
        _repo.Setup(x => x.GetCommitmentsAsync(1, null))
            .ReturnsAsync(new List<CommitmentDto>());

        // When
        var result = await _sut.GetCommitments(1, null);

        // Then
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CommitmentDto>>(okResult.Value);
        Assert.Empty(returnValue);
    }

    [Fact]
    public async Task GetCommitments_Logs_RequestCalls()
    {
        // Given
        var commitments = new Faker<CommitmentDto>().Generate();
        _repo.Setup(x => x.GetCommitmentsAsync(1, null))
            .ReturnsAsync(new List<CommitmentDto> { commitments });

        // When
        await _sut.GetCommitments(1, null);

        // Then


        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Getting commitments list for investor: 1 and assetClass: ")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);

        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Returning commitments list for investor: 1 and assetClass: ")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);
    }

}