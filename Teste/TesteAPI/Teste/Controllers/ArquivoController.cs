using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArquivoController : ControllerBase
    {
        private IArquivoRepository _arquivorepository;

        public ArquivoController(IArquivoRepository arquivoRepository)
        {
            _arquivorepository = arquivoRepository;
        }

        [HttpGet("Get/{idArquivo}")]
        public IActionResult Get(int idArquivo)
        {
            var arquivo = _arquivorepository.Get(idArquivo);
            return Ok(arquivo);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var arquivos = _arquivorepository.GetAll();
            return Ok(arquivos);
        }

        [HttpPost("AddFileToCliente/{idCliente}"), RequestSizeLimit(10737418240)] // 10GB size
        public IActionResult AddFileToCliente(int idCliente)
        {
            try
            {
                var file = Request.Form.Files[0];
                var message = _arquivorepository.ProcessarArquivo(idCliente, file);

                if (string.IsNullOrEmpty(message))
                    return Ok();
                else
                    return StatusCode(500, message);

            }
            catch (IndexOutOfRangeException ex)
            {
                return StatusCode(500, $"O arquivo não foi recebido corretamente. Erro: { ex.Message }");
            }
        }

    }
}