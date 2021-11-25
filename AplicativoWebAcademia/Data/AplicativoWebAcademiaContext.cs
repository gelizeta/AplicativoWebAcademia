using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AplicativoWebAcademia.Models;

namespace AplicativoWebAcademia.Data
{
    public class AplicativoWebAcademiaContext : DbContext
    {
        public AplicativoWebAcademiaContext (DbContextOptions<AplicativoWebAcademiaContext> options)
            : base(options)
        {
        }

        public DbSet<AplicativoWebAcademia.Models.PessoaModel> PessoaModel { get; set; }
    }
}
