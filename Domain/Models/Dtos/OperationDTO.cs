using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos
{
    public class OperationDTO
    {
        public int LeftOperand { get; set; }
        public string Operator { get; set; }
        public int RightOperand { get; set; }
        public int Result { get; set; }

    }
}
