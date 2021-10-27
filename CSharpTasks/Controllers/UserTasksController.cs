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
  public class UserTasksController : ControllerBase
  {
    private readonly UserTasksService _userTasksService;

    public UserTasksController(UserTasksService userTasksService)
    {
      _userTasksService = userTasksService;
    }
  [HttpPost]
  [Authorize]
  public ActionResult<UserTask> Create([FromBody] UserTask data)
  {
    try
    {
       var userTask = _userTasksService.CreateUserTask(data);
       return Ok(userTask);
    }
    catch (System.Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{ id }")]
  [Authorize]

  public async Task<ActionResult<UserTask>> Delete(int id)
  {
    try
    {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      var userTask = _userTasksService.Delete(id, userInfo.Id);
      return Ok(userTask);
    }
    catch (System.Exception e)
    {
     return BadRequest(e.Message);
    }
  }

  }
}