using System.ComponentModel.DataAnnotations;

namespace CSharpTasks.Models
{
  public class UserTask : DbItem<int>
  {
    [Required]
    public int TaskId { get; set; }
    [Required]
    public int UserId { get; set; }
    public Profile User { get; set; }
    public Task Task { get; set; }
  }
}