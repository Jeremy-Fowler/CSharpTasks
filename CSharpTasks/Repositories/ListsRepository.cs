using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CSharpTasks.Interfaces;
using CSharpTasks.Models;
using Dapper;

namespace CSharpTasks.Repositories
{
  public class ListsRepository : DbContext, IRepository<TaskList>
  {
    public ListsRepository(IDbConnection db) : base(db)
    {
    }

    public TaskList Create(TaskList data)
    {
      string sql = @"
      INSERT INTO task_lists(
        name,
        description,
        creatorId
      )
      VALUES(
        @Name,
        @Description,
        @CreatorId
      );
      SELECT LAST_INSERT_ID()
      ;";

      data.Id = _db.ExecuteScalar<int>(sql, data);
      return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM task_lists WHERE id = @id;";

      _db.Execute(sql, new { id });
    }

    public List<TaskList> GetAll()
    {
      var sql = @"
      SELECT
      tl.*
      a.* 
      FROM task_lists tl
      JOINS accounts a ON tl.creatorId = a.id
      ;";

      return _db.Query<TaskList, Profile, TaskList>(sql, (tl, a) =>
      {
        tl.Creator = a;
        return tl;
        }).ToList();
    }

    public TaskList GetById(int id)
    {
      var sql = @"
      SELECT
      tl.*
      a.* 
      FROM task_lists tl
      JOINS accounts a ON tl.creatorId = a.id
      WHERE tl.id = @id
      ;";

      return _db.Query<TaskList, Profile, TaskList>(sql, (tl, a) => 
      {
        tl.Creator = a;
        return tl;
      }, new { id }).FirstOrDefault();
    }

    public TaskList Update(TaskList data)
    {
      var sql = @"
      UPDATE task_lists
      SET
        name = @Name,
        description = @Description
      WHERE id = @Id  
      ;";

      var rowsAffected = _db.Execute(sql, data);
      if(rowsAffected > 1)
      {
        throw new Exception("BAD NEWS BEARS");
      }
      if(rowsAffected == 0)
      {
        throw new Exception("UPDATE FAILED");
      }
      return data;
    }
  }
}