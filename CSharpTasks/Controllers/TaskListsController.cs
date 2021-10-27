using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using CSharpTasks.Models;
using CSharpTasks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpTasks.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TaskListsController : ControllerBase
  {
    private readonly TaskListsService _taskListsService;
    private readonly UserTasksService _userTasksService;
    private readonly TaskItemsService _taskItemsService;

    public TaskListsController(TaskListsService taskListsService, UserTasksService userTasksService, TaskItemsService taskItemsService)
    {
      _taskListsService = taskListsService;
      _userTasksService = userTasksService;
      _taskItemsService = taskItemsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TaskList>> Create([FromBody] TaskList data)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          data.CreatorId = userInfo.Id;
          TaskList taskList = _taskListsService.Create(data);
          return Ok(taskList);
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

    [HttpDelete("{taskListId}")]
    [Authorize]
    public async Task<ActionResult<TaskList>> Delete(int taskListId)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          TaskList taskList = _taskListsService.Delete(taskListId, userInfo.Id);
          return Ok(taskList);   
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{taskListId}")]
    public ActionResult<TaskList> GetById(int taskListId)
    {
      try
      {
          TaskList taskList = _taskListsService.GetById(taskListId);
          return Ok(taskList);  
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpGet]

    public ActionResult<List<TaskList>> GetAll()
    {
      try
      {
          List<TaskList> taskLists = _taskListsService.GetAll();
          return Ok(taskLists);  
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

    [HttpPut("{taskListId}")]
    [Authorize]
    public async Task<ActionResult<TaskList>> Update(int taskListId, [FromBody] TaskList data)
    {
      try
      {
          Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
          TaskList taskList = _taskListsService.Update(userInfo.Id, taskListId, data);
          return Ok(taskList);
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{taskListId}/taskItems")]
    public ActionResult<List<TaskItem>> GetTaskItemsByTaskListId(int taskListId)
    {
      try
      {
       List<TaskItem> taskItems = _taskItemsService.GetTaskItemsByTaskListId(taskListId);
       return Ok(taskItems);
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }


  }
}