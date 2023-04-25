﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
