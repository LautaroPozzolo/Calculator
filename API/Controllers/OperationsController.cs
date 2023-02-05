using API.Services.Contracts;
using Domain.Models;
using Domain.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace API.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly ICalculatorService _calculatorService;

    public OperationsController(ICalculatorService calulatorService)
    {
        _calculatorService = calulatorService;
    }

    // GET: api/<OperationsController>
    /// <summary>
    /// List of operations
    /// </summary>
    /// <returns>All operations</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<Operation>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        return Ok(_calculatorService.GetOperations());
    }

    // POST api/<OperationsController>
    /// <summary>
    /// Calculate operation
    /// </summary>
    ///<remarks>
    /// Sample request:
    ///
    ///     POST api/v1/operations/
    ///
    /// </remarks>
    /// <param name="operation"></param>
    /// <returns>The operation</returns>
    /// <response code="400">If validation criteria are not met</response>
    /// <response code="500">In case of errors occurring on the data layer or anywhere on the server's side</response>
    [HttpPost]
    [ProducesResponseType(typeof(OperationDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody, BindRequired] OperationDTO operation)
    {
        return Created("/api/v1/operations/", _calculatorService.AddOperation(operation));
    }
}
