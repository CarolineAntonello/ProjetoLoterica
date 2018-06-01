using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Domain.Abstract
{
    public interface IRepository<T> where T : Entidade
    {
        T Adicionar(T entidade);

        void Editar(T entidade);

        void Excluir(int Id);

        List<T> GetAll();

        T GetById(int Id);
    }
}
