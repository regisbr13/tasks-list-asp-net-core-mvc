using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Services
{
    // SERVIÇO PARA O CRUD:
    public class TodoService
    {
        private readonly Context _context;

        public TodoService(Context context)
        {
            _context = context;
        }

        // LISTAR TODOS:
        public async Task<List<ToDo>> FindAllAsync()
        {
            return await _context.ToDoes.ToListAsync();
        }

        // ENCONTRAR UM:
        public async Task<ToDo> FindById(int? id)
        {
            return await _context.ToDoes.FirstOrDefaultAsync(t => t.Id == id);
        }

        // INSERIR:
        public async Task InsertAsync(ToDo obj)
        {
            _context.ToDoes.Add(obj);
            await _context.SaveChangesAsync();
        }

        // ATUALIZAR:
        public async Task UpdateAsync(ToDo obj)
        {
            bool hasAny = await _context.ToDoes.AnyAsync(t => t.Id == obj.Id);
            if (!hasAny)
            {
                throw new Exception("not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // REMOVER:
        public async Task RemoveAsync(int id)
        {
            var obj =  await _context.ToDoes.FindAsync(id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();

        }
    }
}
