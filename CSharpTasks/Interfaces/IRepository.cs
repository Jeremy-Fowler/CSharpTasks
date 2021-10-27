using System.Collections.Generic;

namespace CSharpTasks.Interfaces
{
  public interface IRepository<T>
  {
    List<T> GetAll();
    T GetById(int id);
    T Create(T data);
    T Update(T data);
    void Delete(int id);
    
  }
}