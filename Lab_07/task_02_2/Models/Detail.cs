using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace task_02_2.Models
{
    public class Detail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Найменування не може бути порожнім.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Тип повинен бути 'З', 'П' або 'О'.")]
        [RegularExpression("^[ЗПО]$", ErrorMessage = "Тип повинен быть 'З', 'П' или 'О'.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Кількість повинна бути додатним числом.")]
        [Range(1, int.MaxValue, ErrorMessage = "Кількість повинна бути додатним числом.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Вага повинна бути додатним числом.")]
        [Range(1, int.MaxValue, ErrorMessage = "Вага повинна бути додатним числом.")]
        public int Weight { get; set; }
    }
    public class Lab_07_DbContext : DbContext
    {
        public DbSet<Detail> Details { get; set; }
    }
}