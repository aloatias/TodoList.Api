using System;
using System.Threading.Tasks;
using TodoList.Api.Dtos;

namespace TodoList.Api.Interfaces
{
    public interface ITaskService
    {
        Task<AddTaskResult> AddTaskAsync(string taskDescription);

        Task<GetAllTasksResult> GetAllTasksAsync();

        Task<RemoveTaskResult> DeleteTaskAsync(Guid taskId);

        Task<UpdateTaskStatusResult> UpdateTaskStatus(Guid taskId);
    }
}
