using Domain.Models;
using System.Collections.Generic;

namespace API.Repository.Interfaces
{
    public interface ICalculatorRepository
    {
        public List<Operation> GetOperations();

        public Operation AddOperation(Operation operation);
    }
}
