using System;
using System.Data.Entity;

namespace task_01_2.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
    }
    public class Lab_07_DbContext : DbContext
    {
        public DbSet<Detail> Details { get; set; }
    }
}