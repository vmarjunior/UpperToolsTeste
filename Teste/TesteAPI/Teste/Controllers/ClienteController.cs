using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private IClienteRepository _clienterepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienterepository = clienteRepository;
        }

        [HttpGet("Get/{idCliente}")]
        public IActionResult Get(int idCliente)
        {
            var cliente = _clienterepository.Get(idCliente);
            return Ok(cliente);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var clientes = _clienterepository.GetAll();
            return Ok(clientes);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Cliente cliente)
        {
            _clienterepository.Add(cliente);
            _clienterepository.SaveChanges();
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Cliente cliente)
        {
            _clienterepository.Update(cliente);
            _clienterepository.SaveChanges();
            return Ok();
        }

        [HttpDelete("Delete/{idCliente}")]
        public IActionResult Delete(int idCliente)
        {
            _clienterepository.Remove(
                _clienterepository.Get(idCliente));
            _clienterepository.SaveChanges();
            return Ok();
        }

        [HttpGet("GetAllIncludeFilesByFilter/{query}")]
        public IActionResult GetAllIncludeFilesByFilter(string query)
        {
            var clientes = _clienterepository.GetAllIncludeFilesByFilter(query);
            return Ok(clientes);
        }

    }
}