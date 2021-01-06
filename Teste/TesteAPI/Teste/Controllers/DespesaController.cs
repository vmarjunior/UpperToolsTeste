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
    public class DespesaController : ControllerBase
    {
        private IDespesaRepository _despesarepository;

        public DespesaController(IDespesaRepository despesaRepository)
        {
            _despesarepository = despesaRepository;
        }

        [HttpGet("Get/{idDespesa}")]
        public IActionResult Get(int idDespesa)
        {
            var despesa = _despesarepository.Get(idDespesa);
            return Ok(despesa);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var despesas = _despesarepository.GetAll();
            return Ok(despesas);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Despesa despesa)
        {
            _despesarepository.Add(despesa);
            _despesarepository.SaveChanges();
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Despesa despesa)
        {
            _despesarepository.Update(despesa);
            _despesarepository.SaveChanges();
            return Ok();
        }

        [HttpDelete("Delete/{idDespesa}")]
        public IActionResult Delete(int idDespesa)
        {
            _despesarepository.Remove(
                _despesarepository.Get(idDespesa));
            _despesarepository.SaveChanges();
            return Ok();
        }
    }
}