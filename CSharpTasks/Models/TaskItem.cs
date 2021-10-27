using System.ComponentModel.DataAnnotations;

namespace CSharpTasks.Models
{
  
    public class TaskItem : DbItem<int>
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public int ListId { get; set; }
  }
  

}