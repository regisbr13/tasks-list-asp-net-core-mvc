using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Services.Exceptions;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly Context _context;
        public Service(Context context)
        {
            _context = context;
        }

        // Listar todos:
        public async Task<List<TEntity>> FindAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        // Buscar por Id:
        public async Task<TEntity> FindByIdAsync(int? id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        // Inserir elemento:
        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Atualizar elemento:
        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        // Remover elemento:
        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await FindByIdAsync(id);
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não pode ser excluído pois existem itens associados a este elemento.");
            }

        }
    }
}
