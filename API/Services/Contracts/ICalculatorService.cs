using Domain.Models;
using Domain.Models.Dtos;
using System.Collections.Generic;

namespace API.Services.Contracts
{
    public interface ICalculatorService
    {
        public List<Operation> GetOperations();

        public Operation AddOperation(OperationDTO operation);
    }
}
