using Api.Controllers;
using Api.Dtos;
using AutoMapper;
using Entity;
using logger = Microsoft.Extensions.Logging;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.Todo.Tests.UnitTests;

[TestClass]

public class TodosControllerTests
{
    [TestMethod]
    public async Task GetTodos_ReturnsOkResultWithTodoDtos()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);

        var todos = new List<Entity.Todo>(); // Create a list of Todo objects
        var todoDtos = new List<TodoDto>(); // Create a list of TodoDto objects

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.GetTodos())
            .ReturnsAsync(todos);

        // Mock the AutoMapper mapping
        mockMapper.Setup(mapper => mapper.Map<List<TodoDto>>(todos))
            .Returns(todoDtos);

        // Act
        var result = await todosController.GetTodos();


        // Assert
        Assert.IsNotNull(result);
        var okResult = result as ActionResult<List<TodoDto>>;
        Assert.IsNotNull(okResult);
        var returnedTodoDtos = okResult.Value as List<TodoDto>;
    }

    [TestMethod]
    public async Task GetTodo_WithValidId_ReturnsOkResultWithTodoDto()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };
        var todoDto = new TodoDto { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.GetTodo(todoId))
            .ReturnsAsync(todo);

        // Mock the AutoMapper mapping
        mockMapper.Setup(mapper => mapper.Map<TodoDto>(todo))
            .Returns(todoDto);

        // Act
        var result = await todosController.GetTodo(todoId);


        // Assert
        Assert.IsNotNull(result);
        var okResult = result as ActionResult<TodoDto>;
        Assert.IsNotNull(okResult);
        var returnedTodoDto = okResult.Value as TodoDto;
    }

    [TestMethod]
    public async Task GetTodo_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.GetTodo(todoId))
            .ReturnsAsync((Entity.Todo)null);

        // Act
        var result = await todosController.GetTodo(todoId);

        // Assert
        var notFoundResult = result as ActionResult<TodoDto>;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public async Task PostTodo_WithValidTodo_ReturnsCreatedAtActionResultWithTodoDto()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todo = new Entity.Todo { Id = 1 };
        var todoDto = new TodoDto { Id = 1 };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.AddTodo(
                       It.IsAny<Entity.Todo>()))
            .ReturnsAsync(todo);

        // Mock the AutoMapper mapping
        mockMapper.Setup(mapper => mapper.Map<TodoDto>(todo))
            .Returns(todoDto);

        // Act
        var result = await todosController.PostTodo(todo);

        // Assert
        Assert.IsNotNull(result);
        var createdAtActionResult = result as ActionResult<TodoDto>;
        Assert.IsNotNull(createdAtActionResult);
        var returnedTodoDto = createdAtActionResult.Value as TodoDto;
    }

    [TestMethod]
    public async Task PutTodo_WithValidIdAndTodo_ReturnsOkResultWithTodoDto()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };
        var todoDto = new TodoDto { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.UpdateTodo(
                                  todoId, It.IsAny<Entity.Todo>()))
            .ReturnsAsync(todo);

        // Mock the AutoMapper mapping
        mockMapper.Setup(mapper => mapper.Map<TodoDto>(todo))
            .Returns(todoDto);

        // Act
        var result = await todosController.PutTodo(
                                  todoId, todo);

        // Assert
        Assert.IsNotNull(result);
        var okResult = result as ActionResult<TodoDto>;
        Assert.IsNotNull(okResult);
        var returnedTodoDto = okResult.Value as TodoDto;
    }

    [TestMethod]
    public async Task PutTodo_WithInvalidId_ReturnsBadRequestResult()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.UpdateTodo(
                                             todoId, It.IsAny<Entity.Todo>()))
            .ReturnsAsync((Entity.Todo)null);

        // Act
        var result = await todosController.PutTodo(
                                             todoId, todo);

        // Assert
        var badRequestResult = result as ActionResult<TodoDto>;
        Assert.IsNotNull(badRequestResult);
    }


    [TestMethod]
    public async Task DeleteTodo_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.DeleteTodoById(todoId))
            .ReturnsAsync((Entity.Todo)null);

        // Act
        var result = await todosController.DeleteTodo(todoId);

        // Assert
        var notFoundResult = result as NotFoundResult;
        Assert.IsNotNull(notFoundResult);
    }

    [TestMethod]
    public async Task DeleteTodo_WithValidId_ReturnsBadRequestResult()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.DeleteTodoById(todoId))
            .ThrowsAsync(new Exception());

        // Act
        var result = await todosController.DeleteTodo(todoId);

        // Assert
        var badRequestResult = result as BadRequestResult;
        Assert.IsNull(badRequestResult);
    }

    [TestMethod]
    public async Task DeleteTodo_WithValidId_ReturnsNoContentResult()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();

        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.DeleteTodoById(todoId))
            .ReturnsAsync(todo);

        // Act
        var result = await todosController.DeleteTodo(todoId);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
    }

    [TestMethod]
    public async Task DeleteTodo_WithValidId_ReturnsInternalServerErrorResult()
    {
        // Arrange
        var mockTodoRepository = new Mock<ITodoRepository>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<TodosController>>();
        
        var todosController = new TodosController(
                mockTodoRepository.Object,
                mockMapper.Object,
                mockLogger.Object);
        
        var todoId = 1;
        var todo = new Entity.Todo { Id = todoId };

        // Mock the repository method
        mockTodoRepository.Setup(repo => repo.DeleteTodoById(todoId))
            .ThrowsAsync(new Exception());

        // Act
        var result = await todosController.DeleteTodo(todoId);

        // Assert
        Assert.IsNotNull(result);
        var internalServerErrorResult = result as ObjectResult;
        Assert.IsNotNull(internalServerErrorResult);
        Assert.AreEqual(500, internalServerErrorResult.StatusCode);
    }
}