using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IArquivoRepository : IRepository<Arquivo>
    {
        string ProcessarArquivo(int idCliente, IFormFile file);
    }
}
