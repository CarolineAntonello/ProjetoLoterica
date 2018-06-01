using Loterica.Aplication.Abstract;
using Loterica.Domain.Abstract;
using Loterica.Domain.Features.Boloes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Features.Boloes
{
    public class BolaoService : Service<Bolao>
    {
        IBolaoRepository _repository;
        public BolaoService(IBolaoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
