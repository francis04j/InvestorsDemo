using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.DataMappers;
using Preqin.InvestorsApi.DataService;
using Preqin.InvestorsApi.DTOs;

namespace Preqin.InvestorsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestorsController : ControllerBase
{
    private readonly IInvestorRepository _repository;
    private readonly IInvestorDataService _investorDataService;
    private readonly ILogger<InvestorsController> _logger;

    public InvestorsController(IInvestorRepository repository, ILogger<InvestorsController> logger,
        IInvestorDataService investorDataService)
    {
        _repository = repository;
        _logger = logger;
        _investorDataService = investorDataService;
    }

    // GET: api/investors
    [HttpGet]
    public ActionResult<IEnumerable<InvestorSummaryDto>> GetInvestors()
    {
        _logger.LogInformation("Getting investors list");
        var investors = _investorDataService.GetSummaryForAllInvestors();
        _logger.LogInformation("Returning investors list");
        return Ok(investors);
    }
    
    // GET: api/investors/4/commitments
    // GET: api/investors/4/commitments?assetClass=Infrastructure
    [HttpGet("{id}/commitments")]
    public async Task<ActionResult<IEnumerable<CommitmentDto>>> GetCommitments(int id, [FromQuery] string? assetClass)
    {
        _logger.LogInformation("Getting commitments list for investor: {id} and assetClass: {assetClass}", id, assetClass);
        var commitments = await _repository.GetCommitmentsAsync(id, assetClass);
        _logger.LogInformation("Returning commitments list for investor: {id} and assetClass: {assetClass}", id, assetClass);
        return Ok(commitments);
    }
}