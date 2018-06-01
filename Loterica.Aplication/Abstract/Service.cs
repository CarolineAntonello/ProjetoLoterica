using Loterica.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loterica.Aplication.Abstract
{
    public abstract class Service<T> where T : Entidade
    {
        IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void Adicionar(T entidade)
        {
            entidade.Validar();
            _repository.Adicionar(entidade);
        }

        public void Editar(T entidade)
        {
            entidade.Validar();
            _repository.Editar(entidade);
        }

        public void Excluir(T entidade)
        {
            _repository.Excluir(entidade.Id);
        }

        public List<T> PegarTodos()
        {
            return _repository.GetAll();
        }

        public T Get(int id)
        {
            return _repository.GetById(id);
        }

    }
}
