using System.ComponentModel.DataAnnotations;

namespace CSharpTasks.Models
{
  public class TaskList : DbItem<int>
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }

}