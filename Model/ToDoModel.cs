using System.ComponentModel.DataAnnotations;

namespace ToDoService.Model
{
    public class ToDoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }    
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
