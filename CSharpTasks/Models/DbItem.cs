using System;

namespace CSharpTasks.Models
{
  public abstract class DbItem<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}