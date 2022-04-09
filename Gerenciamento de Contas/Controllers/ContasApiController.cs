using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Gerenciamento_de_Contas.Controllers
{
    
    [ApiController]
    public class ContasApiController : ControllerBase
    {
        private readonly Conexoes.SqlServer _Sql;
        
        public ContasApiController()
        {
            _Sql = new Conexoes.SqlServer();
            


    }

        [HttpPost("v1/Contas")]
        public void InserirConta(Entidades.Contas conta)
        {
            _Sql.InserirConta(conta);
           
        }

        [HttpDelete("v1/Contas")]
        public void DeletarConta(Entidades.Contas conta)
        {
            _Sql.DeletarConta(conta);
        }

        [HttpPut("v1/Contas")]
        public void AtualizarConta(Entidades.Contas conta)
        {
            _Sql.AtualizarConta(conta);
        }

       
        [HttpGet("v1/Contas")]
        public List<Entidades.Contas> ListarConta()
        {
            return _Sql.ListarConta();

           

        }
        [HttpGet("v2/Contas")]
        public List<Entidades.Contas> valorTotal()
        {
            return _Sql.valorTotal();
        }

        [HttpGet("v1/Contas/{nome}")]
        public Entidades.Contas ListarConta(string nome)
        {
            return _Sql.SelecionarConta(nome);
        }

        







    }
}
