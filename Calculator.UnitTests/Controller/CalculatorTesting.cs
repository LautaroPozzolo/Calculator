using API.Controllers;
using API.Services.Contracts;
using Domain.Models;
using Domain.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Calculator.UnitTests.Controller
{
    public class CalculatorTesting
    {
        private readonly OperationsController _controller;
        private readonly Mock<ICalculatorService> _service;

        public CalculatorTesting()
        {
            _service = new Mock<ICalculatorService>();
            _controller = new OperationsController(_service.Object);
        }

        [Fact]
        public void GetOperation_Ok()
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

            _service.Setup(m => m.GetOperations()).Returns(operationList);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(operationList, (result as OkObjectResult).Value);

        }

        [Fact]
        public void PostOperation_Ok()
        {
            // Arrange
            var operation = new OperationDTO
            {
                RightOperand = 5,
                LeftOperand = 10,
                Operator = "+",
            };

            var expectedOperation = new Operation
            {
                RightOperand = 5,
                LeftOperand = 10,
                Operator = "+",
                Result = 15
            };

            // Act
            _service.Setup(m => m.AddOperation(operation)).Returns(expectedOperation);
            var result = _controller.Post(operation);

            // Assert
            Assert.IsType<CreatedResult>(result);
            Assert.NotNull(result);
            Assert.Equal((result as CreatedResult).Value, expectedOperation);
        }

    }
}