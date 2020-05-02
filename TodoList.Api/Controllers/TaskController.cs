using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TodoList.Api.Dtos;
using TodoList.Api.Interfaces;

namespace TodoList.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(
            ITaskService taskService,
            ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddTaskAsync([FromBody]AddTask task)
        {
            try
            {
                var addResult = await _taskService.AddTaskAsync(task.TaskDescription);
                switch (addResult.Status)
                {
                    case StatusEnum.Ok:
                        return Created("task", addResult.Task);
                    case StatusEnum.Duplicated:
                        return StatusCode(409, addResult.Error.ErrorMessage);
                    case StatusEnum.InvalidParamater:
                        return BadRequest(addResult.Error.ErrorMessage);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskController) }", $"Method={ nameof(AddTaskAsync) }");
                throw;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            try
            {
                var getAllTasksResult = await _taskService.GetAllTasksAsync();
                switch (getAllTasksResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(getAllTasksResult.Tasks);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskController) }", $"Method={ nameof(GetAllTasksAsync) }");
                throw;
            }
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveTaskAsync(Guid taskId)
        {
            try
            {
                var removeResult = await _taskService.RemoveTaskAsync(taskId);
                switch (removeResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok();
                    case StatusEnum.NotFound:
                        return StatusCode(204, removeResult.Error.ErrorMessage);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskController) }", $"Method={ nameof(RemoveTaskAsync) }");
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody]UpdateTaskStatus task)
        {
            try
            {
                var setTaskToDoneResult = await _taskService.UpdateTaskStatus(task.TaskId);
                switch (setTaskToDoneResult.Status)
                {
                    case StatusEnum.Ok:
                        return Ok(setTaskToDoneResult.Task);
                    case StatusEnum.NotFound:
                        return StatusCode(204, setTaskToDoneResult.Error.ErrorMessage);
                    default:
                        return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskController) }", $"Method={ nameof(GetAllTasksAsync) }");
                throw;
            }
        }
    }
}
