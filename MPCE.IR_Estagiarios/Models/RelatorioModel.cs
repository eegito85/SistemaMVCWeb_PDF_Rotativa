using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPCE.IR_Estagiarios.Models
{
    public class RelatorioModel
    {
        public decimal RendimentosTributaveis { get; set; }

        public decimal AuxilioTransporte { get; set; }

        public string NomeEstagiario { get; set; }

        public string NrCPF { get; set; }

        public int Matricula { get; set; }
    }
}
