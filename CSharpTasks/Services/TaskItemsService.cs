using System;
using System.Collections.Generic;
using CSharpTasks.Models;
using CSharpTasks.Repositories;

namespace CSharpTasks.Services
{
  public class TaskItemsService
  {
    private readonly TasksRepository _tasksRepository;

    public TaskItemsService(TasksRepository tasksRepository)
    {
      _tasksRepository = tasksRepository;
    }

    internal List<TaskItem> GetTaskItemsByTaskListId(int taskListId)
    {
      return _tasksRepository.GetTasksByListId(taskListId);
    }

    internal TaskItem Create(string id, TaskItem data)
    {
      throw new NotImplementedException();
    }

    internal TaskItem Delete(string userId, int taskItemId, int taskListId)
    {
      var taskList = GetById(taskItemId);
      
    }

    internal TaskItem GetById(int taskItemId)
    {
      var taskItem = _tasksRepository.GetById(taskItemId);
      if(taskItem == null)
      {
        throw new Exception("this was not found");
      }
      return taskItem;
    }
  }
}