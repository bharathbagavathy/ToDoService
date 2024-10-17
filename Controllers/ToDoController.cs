using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoService.Data;
using ToDoService.Model;
using ToDoService.Repository.IRepository;
using ToDoService.ResponseMethod;

namespace ToDoService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        APIResponse _response;
        //private readonly ResponseMethods _responseMethod;
        private readonly ToDoDbContext _toDoDbContext;
        private readonly IToDoRepository _toDoRepository;
        public ToDoController(ToDoDbContext toDoDbContext, IToDoRepository toDoRepository)
        {
            _toDoDbContext = toDoDbContext;
            _toDoRepository = toDoRepository;
            this._response = new();
            // _responseMethod = responseMethod;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                IEnumerable<ToDoModel> todolist = await _toDoRepository.GetAllAsync();
                
                _response.Result = todolist;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ToDoModel task = await _toDoRepository.GetAsync(u => u.Id == id);
                if (task == null || task.UserId != userId)
                {
                    return NotFound();
                }

                _response.Result = task;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ToDoModel toDoModel)
        {
            try
            {
                if (toDoModel == null)
                {
                    return BadRequest();
                }
                toDoModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _toDoRepository.CreateAsync(toDoModel);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = toDoModel;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDoModel toDoModel)
        {
            try
            {
                if (id != toDoModel.Id)
                    return BadRequest();

                var existingTask = await _toDoDbContext.Tasks.FindAsync(id);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (existingTask == null || existingTask.UserId != userId)
                {
                    return NotFound();
                }

                existingTask.Title = toDoModel.Title;
                existingTask.Description = toDoModel.Description;

                await _toDoRepository.UpdateAsync(existingTask);

                _response.Result = toDoModel;
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ToDoModel todo = await _toDoDbContext.Tasks.FindAsync(id);

                if (todo == null)
                {
                    return NotFound();
                }

                _toDoDbContext.Tasks.Remove(todo);
                await _toDoDbContext.SaveChangesAsync();
                _response.Result = "Successfully Deleted";
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string> { ex.Message };
                return BadRequest(_response);
            }

        }
    }
}
