using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class DespesaRepository : Repository<Despesa>, IDespesaRepository
    {
        public DespesaRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
