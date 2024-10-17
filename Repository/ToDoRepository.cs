using ToDoService.Data;
using ToDoService.Model;
using ToDoService.Repository.IRepository;

namespace ToDoService.Repository
{
    public class ToDoRepository : Repository<ToDoModel>, IToDoRepository
    {
        public ToDoRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
        }
    }
}
