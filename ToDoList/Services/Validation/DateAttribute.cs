using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoList.Services.Validation
{
    public class DateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ToDo toDo = (ToDo)validationContext.ObjectInstance;
            if (toDo.Initial >= toDo.Final)
                return new ValidationResult(default);
            return ValidationResult.Success;
        }
    }
}
