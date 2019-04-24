using System;
using System.ComponentModel.DataAnnotations;
using ToDoList.Services.Validation;

namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="nome requerido")]
        [Display(Name = "Nome")]
        [StringLength(15, MinimumLength =3, ErrorMessage = "use no máximo {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "data requerida")]
        [DisplayFormat(DataFormatString ="{0: dd/MM/yyyy - HH:mm}")]
        [Display(Name = "Início")]
        public DateTime Initial { get; set; }

        [Date(ErrorMessage = "a data de término deve ser posterior")]
        [Required(ErrorMessage = "data requerida")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy - HH:mm}")]   
        [Display(Name = "Término")]
        public DateTime Final { get; set; }

        [Display(Name = "Prioridade")]
        public string Priority { get; set; }
    }
}
