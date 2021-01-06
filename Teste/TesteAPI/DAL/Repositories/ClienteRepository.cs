using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Cliente> GetAllIncludeFilesByFilter(string query)
        {
            query = query.ToLower();

            return _entities
                .Include(p => p.Arquivos)
                .Where(p => p.NomeCliente.ToLower().Contains(query))
                .Select(p => new Cliente()
                {
                    IdCliente = p.IdCliente,
                    NomeCliente = p.NomeCliente,
                    ArquivosTotal = p.Arquivos.Count
                });
        }
    }
}
