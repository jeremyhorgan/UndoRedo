using System.Collections.Generic;
using UndoRedo.Core;
using UndoRedo.Tests.Fakes;
using UndoRedo.WinForms;
using Xunit;

namespace UndoRedo.Tests.WinForms
{
    public class TestUndoRedoBindingList
    {
        [Fact]
        public void UndoRedoBindingList_WhenTwoItemsAdded_ShouldRemoveOneAfterSingleUndoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);
            var originalCount = accounts.Count;

            // Act
            undoRedoBindingList.Add(new FakeBankAccount(60));
            undoRedoBindingList.Add(new FakeBankAccount(70));
            undoService.Undo();

            // Assert
            Assert.Equal(originalCount + 1, undoRedoBindingList.Count);
        }

        [Fact]
        public void UndoRedoBindingList_WhenTwoItemsAdded_ShouldRemoveAllItemsAfterTwoUndoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);
            var originalCount = accounts.Count;

            // Act
            undoRedoBindingList.Add(new FakeBankAccount(60));
            undoRedoBindingList.Add(new FakeBankAccount(70));
            undoService.Undo();
            undoService.Undo();

            // Assert
            Assert.Equal(originalCount, undoRedoBindingList.Count);
        }

        [Fact]
        public void UndoRedoBindingList_WhenTwoItemsAdded_ShouldHaveTheCorrectCountAfterUndoFollowedByRedoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);
            var originalCount = accounts.Count;

            // Act
            undoRedoBindingList.Add(new FakeBankAccount(60));
            undoRedoBindingList.Add(new FakeBankAccount(70));
            undoService.Undo();
            undoService.Undo();
            undoService.Redo();

            // Assert
            Assert.Equal(originalCount + 1, undoRedoBindingList.Count);
        }

        [Fact]
        public void UndoRedoBindingList_WhenItemDeleted_ShouldRestoreDeletedItemAfterUndoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);
            var originalCount = accounts.Count;

            // Act
            undoRedoBindingList.RemoveAt(2);
            undoService.Undo();

            // Assert
            Assert.Equal(originalCount, undoRedoBindingList.Count);
        }

        [Fact]
        public void UndoRedoBindingList_WhenItemDeleted_ShouldHaveTheCorrectCountAfterUndoFollowedByRedoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);
            var originalCount = accounts.Count;

            // Act
            undoRedoBindingList.RemoveAt(2);
            undoService.Undo();
            undoService.Redo();

            // Assert
            Assert.Equal(originalCount - 1, undoRedoBindingList.Count);
        }

        [Fact]
        public void UndoRedoBindingList_WhenItemMoved_ShouldBeAbleToRestoreAfterUndoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);

            // Act
            undoRedoBindingList[2] = new FakeBankAccount(60);

            var balanceBeforeUndo = undoRedoBindingList[2].Balance;
            undoService.Undo();
            var balanceAfterUndo = undoRedoBindingList[2].Balance;

            // Assert
            Assert.Equal(60, balanceBeforeUndo);
            Assert.Equal(30, balanceAfterUndo);
        }

        [Fact]
        public void UndoRedoBindingList_WhenTwoItemsMoved_ShouldBeAbleToRestoreAfterUndoCommand()
        {
            // Arrange
            var undoService = new UndoRedoService();
            var accounts = FakeBankAccounts.Create();
            var undoRedoBindingList = new UndoRedoBindingList<FakeBankAccount>(undoService, accounts);

            // Act
            undoRedoBindingList[2] = new FakeBankAccount(60);
            undoRedoBindingList[3] = new FakeBankAccount(70);

            var balanceBeforeUndo1 = undoRedoBindingList[2].Balance;
            var balanceBeforeUndo2 = undoRedoBindingList[3].Balance;

            undoService.Undo();
            undoService.Undo();

            var balanceAfterUndo1 = undoRedoBindingList[2].Balance;
            var balanceAfterUndo2 = undoRedoBindingList[3].Balance;

            // Assert
            Assert.Equal(60, balanceBeforeUndo1);
            Assert.Equal(70, balanceBeforeUndo2);

            Assert.Equal(30, balanceAfterUndo1);
            Assert.Equal(40, balanceAfterUndo2);
        }
    }
}
