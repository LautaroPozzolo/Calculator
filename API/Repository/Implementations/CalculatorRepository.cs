using API.Repository.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Implementations
{
    public class CalculatorRepository : ICalculatorRepository
    {
        private readonly CalculatorContext _context;

        public CalculatorRepository(CalculatorContext context)
        {
            _context = context;
        }

        public Operation AddOperation(Operation operation)
        {
            _context.Operations.Add(operation);
            _context.SaveChanges();

            return operation;
        }

        public List<Operation> GetOperations()
        {
            return _context.Operations.ToList();
        }
    }
}
