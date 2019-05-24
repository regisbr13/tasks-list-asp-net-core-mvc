using System;

namespace ToDoList.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string msg) : base(msg)
        {
        }
    }
}
