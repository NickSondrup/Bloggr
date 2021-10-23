using System.ComponentModel.DataAnnotations;

namespace Bloggr.Models
{
  public class Comment
  {
    [Required]
    public int Id { get; set; }
    public string CreatorId { get; set; }
    [Required]
    [MaxLength(240)]
    public string Body { get; set; }
    [Required]
    public int Blog { get; set; }
    public Profile Creator { get; set; } 
    public string Title { get; set; }
    public bool Published { get; set; }
  }
}