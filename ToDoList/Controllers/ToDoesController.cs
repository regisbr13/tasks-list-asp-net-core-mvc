using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    public class ToDoesController : Controller
    {
        private readonly TodoService _toDoService;
        private readonly IMemoryCache _cache;
        
        // Tempo de duração do Cache
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
        // Lista para guardar cache
        private List<ToDo> list;

        public ToDoesController(TodoService toDoService, IMemoryCache cache)
        {
            _toDoService = toDoService;
            _cache = cache;
        }

        // GET:
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue("task", out list))
            {
                list = await _toDoService.FindAllAsync();
                _cache.Set("task", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("task") as List<ToDo>;
            }
            return View(list);
        }

        // GET:
        [HttpGet("/Detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_cache.TryGetValue("task", out list))
            {
                list = await _toDoService.FindAllAsync();
                _cache.Set("task", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("task") as List<ToDo>;
            }
            var toDo = list.Find(t => t.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // CREATE GET:
        [HttpGet("/Nova-tarefa")]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST: 
        [HttpPost("/Nova-tarefa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Initial,Final,Priority")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                TempData["confirm"] = toDo.Name + " criada com sucesso.";
                await _toDoService.InsertAsync(toDo);
                list = await _toDoService.FindAllAsync();
                _cache.Set("task", list, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
            return View(toDo);
        }

        // EDIT GET:
        [HttpGet("/Editar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_cache.TryGetValue("task", out list))
            {
                list = await _toDoService.FindAllAsync();
                _cache.Set("classe", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("task") as List<ToDo>;
            }

            var toDo = list.Find(t => t.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        // EDIT POST:
        [HttpPost("/Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Initial,Final,Priority")] ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["confirm"] = toDo.Name + " atualizada com sucesso.";
                await _toDoService.UpdateAsync(toDo);
                list = await _toDoService.FindAllAsync();
                _cache.Set("task", list, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
            return View(toDo);
        }

        // DELETE GET:
        [HttpGet("/Deletar")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_cache.TryGetValue("task", out list))
            {
                list = await _toDoService.FindAllAsync();
                _cache.Set("classe", list, cacheOptions);
            }
            else
            {
                list = _cache.Get("task") as List<ToDo>;
            }

            var toDo = list.Find(t => t.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // DELETE POST: 
        [HttpPost("/Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _toDoService.FindById(id);
            TempData["confirm"] = obj.Name + " deletada com sucesso.";
            await _toDoService.RemoveAsync(id);
            list = await _toDoService.FindAllAsync();
            _cache.Set("task", list, cacheOptions);
            return RedirectToAction(nameof(Index));
        }
    }
}
