using BLL.Interfaces;
using BLL.Services;
using BLL.Utilities.Validators;
using DAL.Database;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.DTOs;
using Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp.Tests.BLL.Services
{
    public class TodoTaskServiceTest
    {
        private readonly Mock<ITodoTaskRepository> _mockTodoTaskRepository;
        private readonly Mock<ILabelRepository> _mockLabelRepository;
        private readonly TodoTaskService _todoTaskService;
        private readonly TodoListAppDbContext _dbContext;

        public TodoTaskServiceTest()
        {
            var connectionString = "Server=(local); Database=TodoListAppDBTest; Trusted_Connection=True; TrustServerCertificate=True; uid=sa; pwd=12345;";
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var options = new DbContextOptionsBuilder<TodoListAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _mockTodoTaskRepository = new Mock<ITodoTaskRepository>();
            _mockLabelRepository = new Mock<ILabelRepository>();

            var validator = new ValidateTodoTask(_mockTodoTaskRepository.Object, _mockLabelRepository.Object);

            _todoTaskService = new TodoTaskService(new TodoTaskRepository(_dbContext), validator);

        }

        public static IEnumerable<object[]> GetTodoTaskDtos()
        {
            yield return new object[] {
            new TodoTaskDto {
                Title = "Task A",
                Status = "Waiting",
                StartDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(3),
                LabelId = 1
                }
            };
            yield return new object[] {
                new TodoTaskDto {
                    Title = "Task B",
                    Status = "In Progress",
                    StartDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(5),
                    LabelId = 2
                }
            };
        }


        [Theory]
        [MemberData(nameof(GetTodoTaskDtos))]
        public async Task CreateTodoTask_ShouldAddTaskToDatabase(TodoTaskDto todoTaskDto)
        {
            //await _todoTaskService.AddTodoTaskAsync(todoTaskDto);

            var taskInDb = await _dbContext.TodoTasks.FirstOrDefaultAsync(t => t.Title == todoTaskDto.Title);
            Assert.NotNull(taskInDb);
        }


        [Fact]
        [InlineData(1)]
        public async Task GetAll_ShouldReturnAllTasks()
        {
            var listTask = await _todoTaskService.GetAllTodoTasksAsync();

            Assert.NotEmpty(listTask);
            //Assert.Equal(taskId);
        }
    }
}
