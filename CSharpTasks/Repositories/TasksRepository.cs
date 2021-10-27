using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CSharpTasks.Interfaces;
using CSharpTasks.Models;
using Dapper;

namespace CSharpTasks.Repositories
{
  public class TasksRepository : DbContext, IRepository<TaskItem>
  {
    public TasksRepository(IDbConnection db) : base(db)
    {
    }

    public TaskItem Create(TaskItem data)
    {
      string sql = @"
      INSET INTO tasks(name, description, listId)
      VALUES(@Name, @Description, @ListId);
      SELECT LAST_INSERT_ID();
      ";
      data.Id = _db.ExecuteScalar<int>(sql, data);
      return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE from tasks WHERE id = @id;";
      _db.Execute(sql, new { id });
    }

    public List<TaskItem> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public List<TaskItem> GetTasksByListId(int listId)
    {
      string sql = @"
      SELECT t.* FROM tasks t
       WHERE listId = @listId;";
       List<TaskItem> taskItems = _db.Query<TaskItem>(sql, new { listId }).ToList();
       return taskItems;
    }

    public TaskItem GetById(int id)
    {
      string sql = @"
      SELECT * FROM tasks WHERE id = @id;";
      var task = _db.Query(sql, new{ id }).FirstOrDefault();
      return task;
    }

    public TaskItem Update(TaskItem data)
    {
      string sql = @"
      UPDATE tasks
      SET
        name = @Name
        description = @Description
      WHERE id = @Id;
      ";
      var rowsAffected = _db.Execute(sql, data);
    if (rowsAffected > 1)
      {
        throw new Exception("BAD NEWS BEARS");
      }
      if (rowsAffected < 1)
      {
        throw new Exception("this didn't get updated... sry");
      }
      return data;
    }
  }
}