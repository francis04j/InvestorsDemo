using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Preqin.InvestorsApi.Data;
using Preqin.InvestorsApi.DTOs;

namespace Preqin.InvestorsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestorsController : ControllerBase
{
    private readonly IInvestorRepository _repository;
    private readonly ILogger<InvestorsController> _logger;

    public InvestorsController(IInvestorRepository repository, ILogger<InvestorsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // GET: api/investors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvestorSummaryDto>>> GetInvestors()
    {
        _logger.LogInformation("Getting investors list");
        var investors = await _repository.GetInvestorsAsync();
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