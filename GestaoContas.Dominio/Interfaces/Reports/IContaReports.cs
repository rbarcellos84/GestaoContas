using GestaoContas.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.Dominio.Interfaces.Reports
{
    public interface IContaReports
    {
        byte[] CreateExcel(List<Conta> conta);
        byte[] CreatePdf(List<Conta> conta);
    }
}
