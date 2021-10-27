using CSharpTasks.Models;
using CSharpTasks.Repositories;

namespace CSharpTasks.Services
{
  public class UserTasksService
  {
    private readonly UserTasksRepository _usertasksRepository;

    public UserTasksService(UserTasksRepository usertasksRepository)
    {
      _usertasksRepository = usertasksRepository;
    }

    public UserTask CreateUserTask(UserTask data)
    {
      var userTask = _usertasksRepository.Create(data);
      return userTask;
    }

    public UserTask Delete(int userTaskId, string userId)
    {
      var userTask = _usertasksRepository.GetById(userTaskId);
      if(userId != userTask.UserId)
      {
        throw new System.Exception("That is not yours");
      }
      _usertasksRepository.Delete(userTaskId);
      return userTask;      
    }
    
  }
}