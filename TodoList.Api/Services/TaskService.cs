using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Api.DataAccess;
using TodoList.Api.Dtos;
using TodoList.Api.Dtos.Exceptions;
using TodoList.Api.Interfaces;

namespace TodoList.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly TodoListContext _todoListContext;
        private readonly ILogger<TaskService> _logger;

        public TaskService(
            TodoListContext todoListContext,
            ILogger<TaskService> logger)
        {
            _todoListContext = todoListContext;
            _logger = logger;
        }

        public async Task<AddTaskResult> AddTaskAsync(string taskDescription)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(taskDescription))
                {
                    return new AddTaskResult(
                        new InvalidParameterException("The task description can't be empty"),
                        StatusEnum.InvalidParamater
                    );
                }

                var isTaskDuplicated = await _todoListContext.TodoTask.AnyAsync(t => t.Description == taskDescription);
                if (isTaskDuplicated)
                {
                    return new AddTaskResult(
                        new DuplicatedArgumentException($"The task \"{ taskDescription }\" exists already"),
                        StatusEnum.Duplicated
                    );
                }

                var task = new TodoTask
                {
                    Description = taskDescription,
                    CreationDate = DateTime.UtcNow,
                    Done = false
                };

                var createdTask = await _todoListContext.AddAsync(task);
                await _todoListContext.SaveChangesAsync();

                return new AddTaskResult(
                    createdTask.Entity,
                    StatusEnum.Ok
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskService) }", $"Method={ nameof(AddTaskAsync) }");
                return new AddTaskResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<GetAllTasksResult> GetAllTasksAsync()
        {
            try
            {
                return new GetAllTasksResult(
                    await _todoListContext.TodoTask.OrderBy(t => t.CreationDate).ToListAsync(),
                    StatusEnum.Ok
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskService) }", $"Method={ nameof(GetAllTasksAsync) }");
                return new GetAllTasksResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<DeleteTaskResult> DeleteTaskAsync(Guid taskId)
        {
            try
            {
                TodoTask task = await _todoListContext.TodoTask.FirstOrDefaultAsync(t => t.Id == taskId);

                if (task == null)
                {
                    return new DeleteTaskResult(
                        new NotFoundException($"The task Id:\"{ taskId }\" was not found"),
                        StatusEnum.NotFound
                    );
                }

                _todoListContext.Remove(task);
                await _todoListContext.SaveChangesAsync();

                return new DeleteTaskResult(StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskService) }", $"Method={ nameof(DeleteTaskAsync) }");
                return new DeleteTaskResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }

        public async Task<UpdateTaskStatusResult> UpdateTaskStatusAsync(Guid taskId)
        {
            try
            {
                var task = await _todoListContext.TodoTask.FirstOrDefaultAsync(t => t.Id == taskId);
                if (task == null)
                {
                    return new UpdateTaskStatusResult(
                        new NotFoundException($"The task Id: \"{ taskId }\" doesn't exist"),
                        StatusEnum.NotFound
                    );
                }

                if (task.Done)
                {
                    task.Done = false;
                }
                else
                {
                    task.Done = true;
                }
                
                await _todoListContext.SaveChangesAsync();

                return new UpdateTaskStatusResult(
                    task,
                    StatusEnum.Ok
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskService) }", $"Method={ nameof(UpdateTaskStatusAsync) }");
                return new UpdateTaskStatusResult(new InternalServerException(ex.Message), StatusEnum.InternalError);
            }
        }
    }
}
