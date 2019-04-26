using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPCE.IR_Estagiarios.Contexto;
using MPCE.IR_Estagiarios.Models;
using Rotativa.AspNetCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPCE.IR_Estagiarios.Controllers
{
    public class EstagiarioController : Controller
    {
        IConfiguration _iconfiguration;

        public EstagiarioController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IREstagiarioModel dados)
        {
            SIPP acesso = new SIPP();
            RelatorioModel relatorioModel = new RelatorioModel();
            string conexao = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            relatorioModel.RendimentosTributaveis = acesso.CalcularRendimentosTributaveis(dados.AnoRetencao, dados.Matricula, conexao);
            relatorioModel.AuxilioTransporte = acesso.CalcularAuxilioTransporte(dados.AnoRetencao, dados.Matricula, conexao);
            relatorioModel.NomeEstagiario = acesso.RetornarNomeEstagiario(dados.Matricula, conexao);
            relatorioModel.NrCPF = acesso.RetornarCPFEstagiario(dados.Matricula, conexao);
            relatorioModel.Matricula = dados.Matricula;
            var relatorioPDF = new ViewAsPdf
            {
                ViewName = "ComprovanteRendimentos",
                CustomSwitches = "--footer-center \"Comprovante gerado em: " +
                    DateTime.Now.Date.ToString("dd/MM/yyyy") + "  Pag.: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"",
                IsGrayScale = false,
                IsLowQuality = false,
                //FileName = "ComprovanteRendimentos" + dados.Matricula.ToString(),
                Model = relatorioModel
            };
            return relatorioPDF;

            //return View();
        }

    }
}
