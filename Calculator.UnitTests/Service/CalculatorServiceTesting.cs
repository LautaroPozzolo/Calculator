using API.Repository.Interfaces;
using API.Services.CalculatorServices;
using API.Services.Contracts;
using AutoMapper;
using Domain.Models;
using Domain.Models.Dtos;
using Moq;

namespace Calculator.UnitTests.Service
{
    public class CalculatorServiceTesting
    {
        private readonly ICalculatorService _service;
        private readonly Mock<ICalculatorRepository> _repository;
        private readonly Mock<IMapper> _mapper;

        private void MockSetUpMapper(OperationDTO operationDTO)
        {
            _mapper.Setup(m => m.Map<Operation>(operationDTO)).Returns((OperationDTO m) => new Operation
            {
                RightOperand = m.RightOperand,
                LeftOperand = m.LeftOperand,
                Operator = m.Operator,
                Result = m.Result

            });
        }

        public CalculatorServiceTesting()
        {
            _repository = new Mock<ICalculatorRepository>();
            _mapper = new Mock<IMapper>();
            _service = new CalculatorService(_repository.Object, _mapper.Object);
        }

        [Fact]
        public void GetOperation_Ok ()
        {
            // Arrange
            var operationList = new List<Operation>();
            {
                operationList.Add(new Operation()
                {
                    RightOperand = 5,
                    LeftOperand = 5,
                    Operator = ""
                });

                operationList.Add(new Operation()
                {
                    RightOperand = 5,
                    LeftOperand = 5,
                    Operator = "-"
                });
            }

            _repository.Setup(m => m.GetOperations()).Returns(operationList);

            // Act
            var result = _service.GetOperations();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Operation>>(result);
            Assert.Equal(operationList, result);
        }

        [Fact]
        public void PostOperation_Addition()
        {
            // Arrange
            var expected = 10;
            var operationDTO = new OperationDTO
            {
                RightOperand = 3,
                LeftOperand = 7,
                Operator = "+"
            };

            // Act
            MockSetUpMapper(operationDTO);
            _repository.Setup(m => m.AddOperation(It.IsAny<Operation>())).Returns((Operation m) => m);
            var result = _service.AddOperation(operationDTO);

            // Assert
            Assert.IsType<Operation>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Result, expected);
        }

        [Fact]
        public void PostOperation_Substraction()
        {
            // Arrange
            var expected = 2;
            var operationDTO = new OperationDTO
            {
                RightOperand = 5,
                LeftOperand = 7,
                Operator = "-"
            };

            // Act
            MockSetUpMapper(operationDTO);

            _repository.Setup(m => m.AddOperation(It.IsAny<Operation>())).Returns((Operation m) => m);
            
            var result = _service.AddOperation(operationDTO);

            // Assert
            Assert.IsType<Operation>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Result, expected);
        }

        [Fact]
        public void PostOperation_Multiplication()
        {
            // Arrange
            var expected = 35;
            var operationDTO = new OperationDTO
            {
                RightOperand = 5,
                LeftOperand = 7,
                Operator = "*"
            };

            // Act
            MockSetUpMapper(operationDTO);

            _repository.Setup(m => m.AddOperation(It.IsAny<Operation>())).Returns((Operation m) => m);

            var result = _service.AddOperation(operationDTO);

            // Assert
            Assert.IsType<Operation>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Result, expected);
        }

        [Fact]
        public void PostOperation_Division()
        {
            // Arrange
            var expected = 2;
            var operationDTO = new OperationDTO
            {
                RightOperand = 5,
                LeftOperand = 10,
                Operator = "/"
            };

            // Act
            MockSetUpMapper(operationDTO);

            _repository.Setup(m => m.AddOperation(It.IsAny<Operation>())).Returns((Operation m) => m);

            var result = _service.AddOperation(operationDTO);

            // Assert
            Assert.IsType<Operation>(result);
            Assert.NotNull(result);
            Assert.Equal(result.Result, expected);
        }
    }
}
