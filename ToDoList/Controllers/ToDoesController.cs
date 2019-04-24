using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    public class ToDoesController : Controller
    {
        private readonly TodoService _toDoService;

        public ToDoesController(TodoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET:
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            return View(await _toDoService.FindAllAsync());
        }

        // GET:
        [HttpGet("/Detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _toDoService.FindById(id);
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

            var toDo = await _toDoService.FindById(id);
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

            var toDo = await _toDoService.FindById(id);
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
            return RedirectToAction(nameof(Index));
        }
    }
}
