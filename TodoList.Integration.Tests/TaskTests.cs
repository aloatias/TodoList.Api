using NFluent;
using System.Collections.Generic;
using TodoList.Api.DataAccess;
using TodoList.Api.Dtos;
using TodoList.Api.Interfaces;
using Xunit;

namespace TodoList.Integration.Tests
{
    public class TaskTests : TestFactory
    {
        private readonly ITaskService _taskService;

        public TaskTests()
        {
            _taskService = CreateTaskService();
        }

        [Fact]
        public async void Should_AddTask()
        {
            // Prepare
            string taskDescription = "My new Task";

            var expectedResult = new AddTaskResult
            (
                new TodoTask { Description = taskDescription },
                StatusEnum.Ok
            );

            // Act
            var actualResult = await _taskService.AddTaskAsync(taskDescription);

            // Test
            Check.That(actualResult.Status).IsEqualTo(expectedResult.Status);
            Check.That(actualResult.Task.Description).IsEqualTo(expectedResult.Task.Description);
        }

        [Fact]
        public async void Should_GetTwoTasks()
        {
            // Prepare
            string taskDescription1 = "My task 1";
            string taskDescription2 = "My task 2";

            var expectedResult = new GetAllTasksResult
            (
                new List<TodoTask>
                {
                    new TodoTask { Description = taskDescription1 },
                    new TodoTask { Description = taskDescription2 },
                },
                StatusEnum.Ok
            );

            // Act
            await _taskService.AddTaskAsync(taskDescription1);
            await _taskService.AddTaskAsync(taskDescription2);

            var actualResult = await _taskService.GetAllTasksAsync();

            // Test
            Check.That(actualResult.Status).IsEqualTo(expectedResult.Status);

            for (var i = 0; i < actualResult.Tasks.Count; i++)
            {
                Check.That(actualResult.Tasks[i].Description).IsEqualTo(expectedResult.Tasks[i].Description);
            }
        }

        [Fact]
        public async void Should_RemoveTaskById()
        {
            // Prepare
            string taskDescription = "My new Task";

            var expectedResult = new RemoveTaskResult
            (
                StatusEnum.Ok
            );

            // Act
            var addTaskResult = await _taskService.AddTaskAsync(taskDescription);

            var actualResult = await _taskService.RemoveTaskAsync(addTaskResult.Task.Id);

            // Test
            Check.That(actualResult.Status).IsEqualTo(expectedResult.Status);
        }
    }
}
