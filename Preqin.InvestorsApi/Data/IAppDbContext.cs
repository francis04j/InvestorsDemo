using Microsoft.EntityFrameworkCore;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.Data;

public interface IAppDbContext
{
    DbSet<Investor> Investors { get; }
    DbSet<Commitment> Commitments { get; }
}