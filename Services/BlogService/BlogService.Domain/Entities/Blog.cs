using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogService.Domain.Entities.Common;

namespace BlogService.Domain.Entities;

public class Blog : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BlogId { get; set; }
    public string BlogName { get; set; }
    public string BlogDescription { get; set; }
    public string SubName { get; set; }
    public string About { get; set; }
}