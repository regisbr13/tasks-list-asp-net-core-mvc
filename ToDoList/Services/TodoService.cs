using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services
{
    public class TodoService : Service<ToDo>, IToDoService
    {
        public TodoService(Context context) : base(context)
        {
        }
    }
}
