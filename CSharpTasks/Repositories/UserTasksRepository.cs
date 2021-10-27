using System.Collections.Generic;
using System.Data;
using System.Linq;
using CSharpTasks.Interfaces;
using CSharpTasks.Models;
using Dapper;

namespace CSharpTasks.Repositories
{
  public class UserTasksRepository : DbContext, IRepository<UserTask>
  {
    public UserTasksRepository(IDbConnection db) : base(db)
    {
    }

    public UserTask Create(UserTask data)
    {
      var sql = @"
      INSERT INTO user_tasks(
        taskId,
        userId
      )VALUES(
        @TaskId,
        @UserId
      );
      SELECT LAST_INSERT_ID()
      ;";

      data.Id = _db.ExecuteScalar<int>(sql, data);
      return data;
    }

    public void Delete(int id)
    {
      var sql = "DELETE FROM user_tasks WHERE id = @id";

      _db.Execute(sql, new { id });
    }
    
    public List<UserTask> GetTasksByUserId(string userId)
    {
      string sql = @"
      SELECT ut.*, t.*
      FROM user_tasks ut
      JOIN tasks t ON t.id = ut.taskId
      WHERE ut.userId = @userId
      ;";

      return _db.Query<UserTask, TaskItem, UserTask>(sql, (ut, t) => 
      {
        ut.TaskItem = t;
        return ut;
      }, new { userId }).ToList();
    }
    public List<UserTask> GetUsersByTaskId(string taskId)
    {
      string sql = @"
      SELECT ut.*, a.*
      FROM user_tasks ut
      JOIN accounts a ON a.id = ut.userId
      WHERE ut.taskId = @taskId
      ;";

      return _db.Query<UserTask, Profile, UserTask>(sql, (ut, a) => 
      {
        ut.User = a;
        return ut;
      }, new { taskId }).ToList();
    }

    public List<UserTask> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public UserTask GetById(int id)
    {
      throw new System.NotImplementedException();
    }

    public UserTask Update(UserTask data)
    {
      throw new System.NotImplementedException();
    }
  }
}