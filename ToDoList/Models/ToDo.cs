using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        [Display(Name ="Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Início")]
        public DateTime Initial { get; set; }

        [Display(Name = "Término")]
        public DateTime Final { get; set; }

        [Display(Name = "Prioridade")]
        public string Priority { get; set; }
    }
}
