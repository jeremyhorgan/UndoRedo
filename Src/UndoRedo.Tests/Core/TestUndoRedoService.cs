using Moq;
using UndoRedo.Core;
using Xunit;

namespace UndoRedo.Tests.Core
{
    public class TestUndoRedoService
    {
        [Fact]
        public void UndoRedoService_WhenNoUndoRedoHistory_ShouldSetCanUndoCanRedoToFalse()
        {
            // Arrange
            var undoService = new UndoRedoService();

            // Act
            var canUndo = undoService.CanUndo;
            var canRedo = undoService.CanRedo;

            // Assert
            Assert.False(canUndo);
            Assert.False(canRedo);
        }

        [Fact]
        public void UndoRedoService_WhenClearCommandExecuted_ShouldClearUndoRedoHistory()
        {
            // Arrange
            var commandMock = new Mock<IUndoRedoCommand>();
            var undoService = new UndoRedoService();
            undoService.Add(commandMock.Object);

            // Act
            var canUndoBefore = undoService.CanUndo;
            var canRedoBefore = undoService.CanRedo;

            undoService.Clear();

            var canUndoAfter = undoService.CanUndo;
            var canRedoAfter = undoService.CanRedo;

            // Assert
            Assert.True(canUndoBefore);
            Assert.False(canRedoBefore);
            Assert.False(canUndoAfter);
            Assert.False(canRedoAfter);
        }

        [Fact]
        public void UndoRedoService_WhenCommandAdded_ShouldBeAbleToUndoTheCommand()
        {
            // Arrange
            var commandMock = new Mock<IUndoRedoCommand>();
            var undoService = new UndoRedoService();
            undoService.Add(commandMock.Object);

            // Act
            var canUndoBefore = undoService.CanUndo;
            var canRedoBefore = undoService.CanRedo;

            undoService.Undo();

            var canUndoAfter = undoService.CanUndo;
            var canRedoAfter = undoService.CanRedo;

            // Assert
            Assert.True(canUndoBefore);
            Assert.False(canRedoBefore);
            Assert.False(canUndoAfter);
            Assert.True(canRedoAfter);
        }

        [Fact]
        public void UndoRedoService_WhenUndoCommandExecuted_ShouldBeAbleToRedoTheCommand()
        {
            // Arrange
            var commandMock = new Mock<IUndoRedoCommand>();
            var undoService = new UndoRedoService();
            undoService.Add(commandMock.Object);
            undoService.Undo();

            // Act
            var canRedoBefore = undoService.CanRedo;

            undoService.Redo();

            var canRedoAfter = undoService.CanRedo;

            // Assert
            Assert.True(canRedoBefore);
            Assert.False(canRedoAfter);
        }
    }
}
