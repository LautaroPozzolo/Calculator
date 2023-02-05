using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CalculatorContext: DbContext
{
    public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options) { }

    public DbSet<Operation> Operations { get; set; }
}
