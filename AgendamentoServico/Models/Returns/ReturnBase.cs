using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoServico.Models
{
    public class ReturnBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public void Reset()
        {
            this.IsSuccess = false;
            this.Message = string.Empty;
        }   
    }
}
