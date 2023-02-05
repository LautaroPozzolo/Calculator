using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Operation
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public int LeftOperand { get; set; }
    public string Operator { get; set; }
    public int RightOperand { get; set; }
    public int Result { get; set; }
}
