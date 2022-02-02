using ContaCorrente.ApiExtrato.Data;
using ContaCorrente.ApiExtrato.Enums;
using ContaCorrente.ApiExtrato.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContaCorrente.ApiExtrato.Controllers
{

    [ApiController]
    [Route("v1/extrato")]
    public class TransacoesController : ControllerBase
    {
        private readonly TransacoesDataContext _TransacaoDataContext;

        public TransacoesController(TransacoesDataContext TransacoesDataContext)
        {
            _TransacaoDataContext = TransacoesDataContext;
        }

        [HttpGet("{dias:int}")]
        public async Task<IActionResult> Get(
            [FromRoute] int dias,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
        )
        {
            try
            {
                var transacoes = await _TransacaoDataContext.GetTransacoesDia(dias);

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = transacoes.Count,
                    page,
                    pageSize,
                    transacoes
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna servidor"));

            }
        }

        [HttpGet("{agencia}/{conta}")]
        public async Task<IActionResult> GetAgenciaConta(
            [FromRoute] string agencia,
            [FromRoute] string conta,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
        )
        {
            try
            {
                var transacoes = await _TransacaoDataContext.GetTransacoesAgenciaConta(agencia, conta);

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = transacoes.Count,
                    page,
                    pageSize,
                    transacoes
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna servidor"));

            }
        }

        [HttpGet("{agencia}/{conta}/{dias}")]
        public async Task<IActionResult> GetAgenciaContaDias(
            [FromRoute] string agencia,
            [FromRoute] string conta,
            [FromRoute] int dias,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25
        )
        {
            try
            {
                var transacoes = await _TransacaoDataContext.GetTransacoesAgenciaContaDia(agencia, conta, dias);

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = transacoes.Count,
                    page,
                    pageSize,
                    transacoes
                }));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna servidor"));

            }
        }

    }
}