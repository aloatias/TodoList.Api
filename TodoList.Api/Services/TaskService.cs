using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
                        new NullArgumentException("The task description can't be empty"),
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

                var task = new TodoTask { Description = taskDescription };

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
                return new AddTaskResult(StatusEnum.InternalError);
            }
        }

        public async Task<GetAllTasksResult> GetAllTasksAsync()
        {
            try
            {
                return new GetAllTasksResult(await _todoListContext.TodoTask.ToListAsync(), StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskService) }", $"Method={ nameof(GetAllTasksAsync) }");
                return new GetAllTasksResult(StatusEnum.InternalError);
            }
        }

        public async Task<RemoveTaskResult> RemoveTaskAsync(Guid taskId)
        {
            try
            {
                TodoTask task = await _todoListContext.TodoTask.FirstOrDefaultAsync(t => t.Id == taskId);

                if (task == null)
                {
                    return new RemoveTaskResult(
                        new NotFoundException($"The task Id:\"{ taskId }\" was not found"),
                        StatusEnum.Duplicated
                    );
                }

                _todoListContext.Remove(task);
                await _todoListContext.SaveChangesAsync();

                return new RemoveTaskResult(StatusEnum.Ok);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException, $"Class={ nameof(TaskService) }", $"Method={ nameof(RemoveTaskAsync) }");
                return new RemoveTaskResult(StatusEnum.InternalError);
            }
        }
    }
}
