using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TodoList.Api.DataAccess;
using TodoList.Api.Interfaces;
using TodoList.Api.Services;

namespace TodoList.Integration.Tests
{
    public class TestFactory
    {
        protected ITaskService CreateTaskService()
        {
            return new TaskService(CreateContext(), CreateLogger<TaskService>());
        }

        private ILogger<T> CreateLogger<T>()
        {
            var loggerFactory = new LoggerFactory();

            return loggerFactory.CreateLogger<T>();
        }

        private TodoListContext CreateContext()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TodoListContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider.GetRequiredService<TodoListContext>();
        }
    }
}
