using API.Models.Exceptions;
using API.Repository.Interfaces;
using API.Services.Contracts;
using AutoMapper;
using Domain.Models;
using Domain.Models.Dtos;
using System.Collections.Generic;
using System.Net;

namespace API.Services.CalculatorServices
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculatorRepository _repository;
        private readonly IMapper _mapper;

        public CalculatorService(ICalculatorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Operation AddOperation(OperationDTO operation)
        {
            switch (operation.Operator)
            {
                case "+":
                    operation.Result = operation.LeftOperand + operation.RightOperand;
                    break;
                case "-":
                    operation.Result = operation.LeftOperand - operation.RightOperand;
                    break;
                case "*":
                    operation.Result = operation.LeftOperand * operation.RightOperand;
                    break;
                case "/":
                    operation.Result = operation.LeftOperand / operation.RightOperand;
                    break;
                default:
                    throw new CustomResponseException(HttpStatusCode.BadRequest, $"Operation {operation.Operator} not allowed");
            }
            var operationMapper = _mapper.Map<Operation>(operation);

            return _repository.AddOperation(operationMapper);
        }

        public List<Operation> GetOperations()
        {
            return _repository.GetOperations();
        }
    }
}
