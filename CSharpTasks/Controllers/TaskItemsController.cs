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
  public class TaskItemsController : ControllerBase
  {
    private readonly TaskItemsService _taskItemsService;

    public TaskItemsController(TaskItemsService taskItemsService)
    {
      _taskItemsService = taskItemsService;
    }
    [HttpPost]
    [Authorize]

    public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem data)
    {
      try
      {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      TaskItem taskItem = _taskItemsService.Create(userInfo.Id, data);
      return Ok(taskItem);
           
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpDelete("{taskItemId}")]
    [Authorize]
    public async Task<ActionResult<TaskItem>> Delete(int taskItemId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        TaskItem taskItem = _taskItemsService.Delete(userInfo.Id, taskItemId);
        return Ok(taskItem);
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
    [HttpGet("{taskItemId}")]
    public ActionResult<TaskItem> GetById(int taskItemId)
    {
      try
      {
        TaskItem taskItem = _taskItemsService.GetById(taskItemId);
        return Ok(taskItem);
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
  }
}