using System;
using System.Collections.Generic;
using CSharpTasks.Models;
using CSharpTasks.Repositories;

namespace CSharpTasks.Services
{
  public class TaskListsService
  {
    private readonly ListsRepository _taskListsRepository;

    public TaskListsService(ListsRepository taskListsRepository)
    {
      _taskListsRepository = taskListsRepository;
    }

    internal TaskList Create(TaskList data)
    {
      return _taskListsRepository.Create(data);
    }

    internal TaskList Delete(int taskListId, string userId)
    {
      TaskList taskList = GetById(taskListId);
      if(taskList.CreatorId != userId)
      {
        throw new Exception("YOU CAN'T DO IT");
      }
      _taskListsRepository.Delete(taskListId);
      return taskList;
    }

    internal TaskList GetById(int taskListId)
    {
      TaskList taskList = _taskListsRepository.GetById(taskListId);
      if(taskList == null)
      {
        throw new Exception("No TaskList found, baby");
      }
      return taskList;
    }

    internal List<TaskList> GetAll()
    {
      return _taskListsRepository.GetAll();
    }

    internal TaskList Update(string userId, int taskListId, TaskList data)
    {
      TaskList taskList = GetById(taskListId);
      if(taskList.CreatorId != userId)
      {
        throw new Exception("YOU CAN'T DO IT");
      }
      taskList.Name = data.Name ?? taskList.Name;
      taskList.Description = data.Description ?? taskList.Description;
      return _taskListsRepository.Update(taskList);
    }
  }
}