using System;
using System.Collections.Generic;
using CSharpTasks.Models;
using CSharpTasks.Repositories;

namespace CSharpTasks.Services
{
  public class TaskItemsService
  {
    private readonly TasksRepository _tasksRepository;
    private readonly TaskListsService _taskListsService;

    public TaskItemsService(TasksRepository tasksRepository, TaskListsService taskListsService)
    {
      _tasksRepository = tasksRepository;
      _taskListsService = taskListsService;
    }

    internal List<TaskItem> GetTaskItemsByTaskListId(int taskListId)
    {
      return _tasksRepository.GetTasksByListId(taskListId);
    }

    internal TaskItem Create(string id, TaskItem data)
    {
      throw new NotImplementedException();
    }

    internal TaskItem Delete(string userId, int taskItemId, TaskItem data)
    {
      if(taskItemId != data.Id)
      {
        throw new Exception("NO WAY, JOSIE");
      }
      var taskList = _taskListsService.GetById(data.ListId);
      if (taskList.CreatorId != userId)
      {
        throw new Exception("NO WAY, JOSIE");
      }
      var taskItem = GetById(taskItemId);
      _tasksRepository.Delete(taskItemId);
      return taskItem;
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